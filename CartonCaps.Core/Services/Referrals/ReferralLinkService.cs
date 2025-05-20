using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.Persistence.Models;
using CartonCaps.Persistence.Repositories;

namespace CartonCaps.Core.Services.Referrals
{

    /// <inheritdoc cref="IReferralLinkService"/>
    public class ReferralLinkService : IReferralLinkService
    {
        protected readonly IReferralLinkRepository referralLinkRepository;
        protected readonly IDeferredLinkService deferredLinkService;

        public ReferralLinkService(IReferralLinkRepository referralLinkRepository, IDeferredLinkService deferredLinkService)
        {
            this.referralLinkRepository = referralLinkRepository;
            this.deferredLinkService = deferredLinkService;
        }

        public async Task<string> FetchValidReferralLink(CartonCapsUser user, CancellationToken cancellationToken)
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
    }
}
