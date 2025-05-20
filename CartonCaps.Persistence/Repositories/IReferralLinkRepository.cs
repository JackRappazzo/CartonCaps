using CartonCaps.Persistence.Models;

namespace CartonCaps.Persistence.Repositories
{
    /// <summary>
    /// Stores and retrieves referral links from the repository
    /// </summary>
    public interface IReferralLinkRepository
    {  
        /// <summary>
       /// Fetches the most recent referral link that is still valid.
       /// If none are found it will return null
       /// </summary>
       /// <param name="userId">Guid representing the user</param>
       /// <param name="cancellationToken"></param>
       /// <returns></returns>
        Task<ReferralLink?> FetchUnexpiredReferralLinkByUserId(Guid userId, CancellationToken cancellationToken);
       
        /// <summary>
        /// Inserts a new referral link to the repository
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="url"></param>
        /// <param name="expiration"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InsertReferralLink(Guid userId, string url, DateTime expiration, CancellationToken cancellationToken);
    }
}