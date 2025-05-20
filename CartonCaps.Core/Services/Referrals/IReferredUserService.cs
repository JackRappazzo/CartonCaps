using CartonCaps.Persistence.Models;

namespace CartonCaps.Core.Services.Referrals
{
    /// <summary>
    /// A service to handle fetching referred user data for display to the end user
    /// </summary>
    public interface IReferredUserService
    {
        /// <summary>
        /// Fetches a collection of referrals for a given user, sorted by status then the time the referral was created, with the most recent first
        /// </summary>
        /// <remarks>This collection will conflate <see cref="ReferralState.NeedsAudit"/> and <see cref="ReferralState.Pending"/> so that bad actors do not know when they were detected
        /// Further, this collection will omit <see cref="ReferralState.Denied"/> results</remarks>
        /// <param name="userId">The user whose referrals we are fetching</param>
        /// <param name="skip">The number of results to skip for the purposes of paging</param>
        /// <param name="take">The number of results to include for the purposes of paging</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<(int Total, IEnumerable<ReferredUser> Referrals)> GetUserReferralsById(Guid userId, int skip, int take, CancellationToken cancellationToken);
    }
}