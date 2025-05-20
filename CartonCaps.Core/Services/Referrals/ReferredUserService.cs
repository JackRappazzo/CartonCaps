using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using CartonCaps.Persistence.Repositories;

namespace CartonCaps.Core.Services.Referrals
{
    public class ReferredUserService
    {
        protected IReferralRepository referralRepository;

        public ReferredUserService(IReferralRepository referralRepository)
        {
            this.referralRepository = referralRepository;
        }

        /// <summary>
        /// Fetches a collection of referrals for a given user, sorted by status then the time the referral was created, with the most recent first
        /// </summary>
        /// <param name="userId">The user whose referrals we are fetching</param>
        /// <param name="skip">The number of results to skip for the purposes of paging</param>
        /// <param name="take">The number of results to include for the purposes of paging</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<(int total, IEnumerable<ReferredUser> referrals)> GetUserReferralsById(Guid userId, int skip, int take, CancellationToken cancellationToken)
        {
            //Pull everything and remove those who have been rejected 
            var referrals = (await referralRepository.GetReferredUsersByReferringId(userId, cancellationToken))
                ?.Where(r => r.ReferralState != ReferralState.Denied) ?? new ReferredUser[0];

            var resultSet = referrals
                .Skip(skip)
                .Take(take)
                .Select(r =>
                {
                    //Bad actors can use the NeedsAudit state to know when their fraud methods
                    //have been compromised. Let's hide this fact for now
                    if (r.ReferralState == ReferralState.NeedsAudit)
                    {
                        r.ReferralState = ReferralState.Pending;
                    }
                    return r;
                })
                .OrderBy(r => r.ReferralState)
                .ThenBy(r => r.CreatedOn);

            return (referrals.Count(), resultSet);
        }

    }
}
