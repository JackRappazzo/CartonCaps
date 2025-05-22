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
        protected readonly IDeferredLinkService deferredLinkService;
        protected readonly ILogger<ReferralLinkService> logger;

        public ReferralLinkService(IReferralLinkRepository referralLinkRepository, IDeferredLinkService deferredLinkService, ILogger<ReferralLinkService> logger)
        {
            this.referralLinkRepository = referralLinkRepository;
            this.deferredLinkService = deferredLinkService;
            this.logger = logger;
        }

        public async Task<string> FetchValidReferralLink(CartonCapsUser user, CancellationToken cancellationToken)
        {
            try
            {
                var activeReferralLink = await referralLinkRepository.FetchUnexpiredReferralLinkByUserId(user.Id, cancellationToken);

                if (activeReferralLink == null)
                {
                    var referralCode = user.ReferralCode;

                    var newLink = await deferredLinkService.CreateReferralDeepLink(referralCode, cancellationToken);

                    await referralLinkRepository.InsertReferralLink(user.Id, newLink, DateTime.Now + TimeSpan.FromDays(60), cancellationToken);
                    return newLink;
                }
                else
                {
                    return activeReferralLink.Url;
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching referral links for user: {userId}. Returning empty string", user.Id);
                return string.Empty;
            }
        }
    }
}
