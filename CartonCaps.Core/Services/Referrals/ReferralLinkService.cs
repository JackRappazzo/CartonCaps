using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.Persistence.Models;
using CartonCaps.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace CartonCaps.Core.Services.Referrals
{

    /// <inheritdoc cref="IReferralLinkService"/>
    public class ReferralLinkService : IReferralLinkService
    {
        protected readonly IReferralLinkRepository referralLinkRepository;
        protected readonly IUserRepository userRepository;
        protected readonly IDeferredLinkService deferredLinkService;
        protected readonly ILogger<ReferralLinkService> logger;

        public ReferralLinkService(IReferralLinkRepository referralLinkRepository, IUserRepository userRepository, IDeferredLinkService deferredLinkService, ILogger<ReferralLinkService> logger)
        {
            this.referralLinkRepository = referralLinkRepository;
            this.userRepository = userRepository;
            this.deferredLinkService = deferredLinkService;
            this.logger = logger;
        }

        public async Task<(string referralCode, string deferredLink)> FetchReferralCodeAndValidLink(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var activeReferralLink = await referralLinkRepository.FetchUnexpiredReferralLinkByUserId(userId, cancellationToken);
                var referralCode = await userRepository.FetchUsersReferralCode(userId, cancellationToken);

                if (activeReferralLink == null)
                {
                    if(string.IsNullOrEmpty(referralCode))
                    {
                        logger.LogWarning("User with ID {userId} does not exist or does not have a referral code. Cannot create referral link.", userId);
                        return (string.Empty, string.Empty);
                    }

                    var newLink = await deferredLinkService.CreateReferralDeepLink(referralCode, cancellationToken);

                    newLink += "?referral_code=" + referralCode;

                    await referralLinkRepository.InsertReferralLink(userId, newLink, DateTime.Now + TimeSpan.FromDays(60), cancellationToken);
                    return (referralCode, newLink);
                }
                else
                {
                    return (referralCode ?? string.Empty, activeReferralLink.Url);
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching referral links for user: {userId}. Returning empty string", userId);
                return (string.Empty, string.Empty);
            }
        }
    }
}
