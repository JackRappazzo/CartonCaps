using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using CartonCaps.Persistence.Repositories;

namespace CartonCaps.Core.Services.Referrals
{
    /// <inheritdoc cref="IReferredUserService"/>
    public class ReferredUserService : IReferredUserService
    {
        protected IReferralRepository referralRepository;

        public ReferredUserService(IReferralRepository referralRepository)
        {
            this.referralRepository = referralRepository;
        }

        
        public async Task<(int Total, IEnumerable<ReferredUser> Referrals)> GetUserReferralsById(Guid userId, int skip, int take, CancellationToken cancellationToken)
        {
            //Pull everything and remove those who have been rejected 
            var referrals = (await referralRepository.GetReferredUsersByReferringId(userId, cancellationToken))
                ?.Where(r => r.ReferralState != ReferralState.Denied) ?? new ReferredUser[0];

            var resultSet = referrals
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
                .ThenBy(r => r.CreatedOn)
                .Skip(skip)
                .Take(take);

            return (referrals.Count(), resultSet);
        }

    }
}
