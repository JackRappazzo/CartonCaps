using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using CartonCaps.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace CartonCaps.Core.Services.Referrals
{
    /// <inheritdoc cref="IReferredUserService"/>
    public class ReferredUserService : IReferredUserService
    {
        protected IReferralRepository referralRepository;
        protected ILogger<ReferredUserService> logger;

        public ReferredUserService(IReferralRepository referralRepository, ILogger<ReferredUserService> logger)
        {
            this.referralRepository = referralRepository;
            this.logger = logger;
        }

        
        public async Task<(int Total, IEnumerable<ReferredUser> Referrals)> GetUserReferralsById(Guid userId, int skip, int take, CancellationToken cancellationToken)
        {
            try
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
                    .OrderBy(r =>
                    {
                        //TODO: Encapsulate this somewhere
                        //I don't want to order by the enum value directly
                        return r.ReferralState switch
                        {
                            ReferralState.Completed => 0,
                            ReferralState.Pending => 1,
                            ReferralState.NeedsAudit => 2,
                            ReferralState.Denied => 3,
                            _ => 4
                        };
                    })
                    .ThenBy(r => r.CreatedOn)
                    .Skip(skip)
                    .Take(take);

                return (referrals.Count(), resultSet);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An exception occurred while fetching user referrals for User: {userId}. Returning empty set", userId);
                return (0, []);
            }
        }

    }
}
