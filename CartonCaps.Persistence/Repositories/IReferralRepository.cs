﻿using CartonCaps.Persistence.Models;

namespace CartonCaps.Persistence.Repositories
{
    /// <summary>
    /// Manages referrals in persistent storage
    /// </summary>
    public interface IReferralRepository
    {
        /// <summary>
        /// Retreives the valid referrals for a specific user
        /// </summary>
        /// <param name="referringUserId">The user who made the referrals</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ReferredUser>> GetReferredUsersByReferringId(Guid referringUserId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an individual referrals <see cref="ReferralState"/>
        /// </summary>
        /// <param name="referralId">The referral ID to update</param>
        /// <param name="newReferralState">The new state for this referral</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UpdateReferralStateById(Guid referralId, ReferralState newReferralState, CancellationToken cancellationToken);
    }
}