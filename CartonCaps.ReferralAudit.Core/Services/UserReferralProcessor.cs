using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using CartonCaps.Persistence.Repositories;

namespace CartonCaps.ReferralAudit.Core.Services
{
    public class UserReferralProcessor
    {
        private readonly IUserRepository userRepository;
        private readonly IReferralRepository referralRepository;
        private readonly IIndividualReferralStateEvaluator individualReferralStateEvaluator;
        private readonly AuditThresholdConfiguration thresholdConfiguration;


        public UserReferralProcessor(
            IUserRepository userRepository, 
            IReferralRepository referralRepository, 
            IIndividualReferralStateEvaluator stateEvaluator, 
            IAuditThresholdConfigurationFactory configFactory)
        {
            this.userRepository = userRepository;
            this.referralRepository = referralRepository;
            this.individualReferralStateEvaluator = stateEvaluator;
            this.thresholdConfiguration = configFactory.Create();
        }

        public async Task UpdateUsersReferrals(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userRepository.FetchUserById(userId, cancellationToken);
            var userReferrals = await referralRepository.GetReferredUsersByReferringId(userId, cancellationToken);

            var ipThresholdFailureGuids = GetReferredIdsOverIpThreshold(user.RegisteredIpAddress, userReferrals);
            var sessionThresholdFailureGuids = GetReferredIdsOverSessionThreshold(user.RegisteredSessionId, userReferrals);

            var referralIdsToAudit = ipThresholdFailureGuids.Concat(sessionThresholdFailureGuids).Distinct();

            //Remove all of referrals heading to audit and keep only those that are pending
            //We don't need to process completed referrals
            var referralsToProcess = userReferrals.Where(r => 
                !referralIdsToAudit.Contains(r.Id) && 
                r.ReferralState == ReferralState.Pending);

            foreach(var referral in referralsToProcess)
            {
                var referralState = await individualReferralStateEvaluator.EvaluateReferralState(referral, cancellationToken);
                
                var success = await referralRepository.UpdateReferralStateById(referral.Id, referralState, cancellationToken);
                if(!success)
                {
                    // Log as an error and continue
                    //We do not want to throw if we can avoid it
                }

            }

            foreach(var filtered in userReferrals.Where(r=>referralIdsToAudit.Contains(r.Id)))
            {
                var success = await referralRepository.UpdateReferralStateById(filtered.Id, ReferralState.NeedsAudit, cancellationToken);
                if (!success)
                {
                    // Log as an error and continue
                    //We do not want to throw if we can avoid it
                }
            }

        }

        private IEnumerable<Guid> GetReferredIdsOverIpThreshold(string ipAddress, IEnumerable<ReferredUser> referrals)
        {
            var referralsSharingIpAddress = referrals.Where(r => r.ReferredIpAddress == ipAddress);
            if (referralsSharingIpAddress.Count() > thresholdConfiguration.SameIpThreshold)
            {
                return referralsSharingIpAddress.Select(r => r.Id);
            }
            else return [];
        }

        private IEnumerable<Guid> GetReferredIdsOverSessionThreshold(Guid sessionId, IEnumerable<ReferredUser> referrals)
        {
            var referralsSharingSessionId = referrals.Where(r => r.ReferredSessionId == sessionId);
            if (referralsSharingSessionId.Count() > thresholdConfiguration.SameSessionThreshold)
            {
                return referralsSharingSessionId.Select(r => r.Id);
            }
            else return [];
        }

    }
}
