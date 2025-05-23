﻿using CartonCaps.Persistence.Models;

namespace CartonCaps.Core.Services.Referrals
{
    /// <summary>
    /// Ensures that a valid referral link can be retrieved
    /// Handles expiration and renewal
    /// </summary>
    public interface IReferralLinkService
    {

        /// <summary>
        /// Ensures that a valid referral link can be retrieved
        /// Handles expiration and renewal
        /// </summary>
        Task<string> FetchValidReferralLink(CartonCapsUser user, CancellationToken cancellationToken);
    }
}