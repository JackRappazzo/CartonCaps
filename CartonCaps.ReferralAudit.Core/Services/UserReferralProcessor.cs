using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using CartonCaps.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace CartonCaps.ReferralAudit.Core.Services
{
    public class UserReferralProcessor
    {
        private readonly IUserRepository userRepository;
        private readonly IReferralRepository referralRepository;
        private readonly IIndividualReferralStateEvaluator individualReferralStateEvaluator;
        private readonly IIpThresholdEvaluator ipThresholdEvaluator;
        private readonly ISessionIdThresholdEvaluator sessionIdThresholdEvaluator;
        private readonly AuditThresholdConfiguration thresholdConfiguration;
        private readonly ILogger<UserReferralProcessor> logger;


        public UserReferralProcessor(
            IUserRepository userRepository,
            IReferralRepository referralRepository,
            IIndividualReferralStateEvaluator stateEvaluator,
            IIpThresholdEvaluator ipThresholdEvaluator,
            ISessionIdThresholdEvaluator sessionIdThresholdEvaluator,
            IAuditThresholdConfigurationFactory configFactory,
            ILogger<UserReferralProcessor> logger)
        {
            this.userRepository = userRepository;
            this.referralRepository = referralRepository;
            this.individualReferralStateEvaluator = stateEvaluator;
            this.ipThresholdEvaluator = ipThresholdEvaluator;
            this.sessionIdThresholdEvaluator = sessionIdThresholdEvaluator;
            this.thresholdConfiguration = configFactory.Create();
            this.logger = logger;
        }

        public async Task UpdateUsersReferrals(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userRepository.FetchUserById(userId, cancellationToken);
            var userReferrals = await referralRepository.GetReferredUsersByReferringId(userId, cancellationToken);

            //Only proceed if we have referrals that are Pending
            //We do NOT filter out Completed referrals yet since they are still
            //relevant to our filters
            if (userReferrals.Where(r => r.ReferralState == ReferralState.Pending).Any())
            {

                var ipThresholdFailureGuids = ipThresholdEvaluator.GetReferralIdsThatExceedIpThreshold(
                    user.RegisteredIpAddress,
                    userReferrals,
                    thresholdConfiguration.SameIpThreshold);

                var sessionThresholdFailureGuids = sessionIdThresholdEvaluator.GetReferralIdsThatExceedSessionIdThreshold(
                    user.RegisteredSessionId,
                    userReferrals,
                    thresholdConfiguration.SameSessionThreshold);



                var referralIdsToAudit = ipThresholdFailureGuids.Concat(sessionThresholdFailureGuids).Distinct();

                //Remove all of referrals heading to audit and keep only those that are pending
                //We don't need to process completed referrals
                var referralsToProcess = userReferrals.Where(r =>
                    !referralIdsToAudit.Contains(r.Id) &&
                    r.ReferralState == ReferralState.Pending);

                foreach (var referral in referralsToProcess)
                {
                    var referralState = await individualReferralStateEvaluator.EvaluateReferralState(referral, cancellationToken);

                    if (referralState == ReferralState.Completed)
                    {
                        var success = await referralRepository.UpdateReferralStateById(referral.Id, referralState, cancellationToken);
                        if (!success)
                        {
                            // Log as an error and continue
                            //We do not want to throw if we can avoid it
                            logger.LogError("Failed to update referral state of referral {referralId} for user {userId}. Continuing", user.Id, referral.Id);
                        }
                    }

                }

                foreach (var filtered in userReferrals.Where(r => referralIdsToAudit.Contains(r.Id)))
                {
                    var success = await referralRepository.UpdateReferralStateById(filtered.Id, ReferralState.NeedsAudit, cancellationToken);
                    if (!success)
                    {
                        // Log as an error and continue
                        //We do not want to throw if we can avoid it
                        logger.LogError("Failed to update referral state of referral {referralId} for user {userId}. Continuing", user.Id, filtered.Id);
                    }
                }
            }
        }
    }
}