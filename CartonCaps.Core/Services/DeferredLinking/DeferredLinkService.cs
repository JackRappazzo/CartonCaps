using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CartonCaps.Core.Services.DeferredDeepLinking
{
    /// <inheritdoc cref="IDeferredLinkService" />
    public class DeferredLinkService : IDeferredLinkService
    {
        private readonly IDeepLinkClient deepLinkClient;
        private readonly ILogger<DeferredLinkService> logger;

        public DeferredLinkService(IDeepLinkClient deepLinkClient, ILogger<DeferredLinkService> logger)
        {
            this.deepLinkClient = deepLinkClient;
            this.logger = logger;
        }

        public async Task<string> CreateReferralDeepLink(string referralCode, CancellationToken cancellationToken)
        {
            try
            {
                var metaData = new Dictionary<string, object>();
                metaData.Add("ReferralCode", referralCode);

                var url = await deepLinkClient.CreateDeepLink(DeferredLinkDestinations.ReferralRegistration, metaData, cancellationToken);

                return url;
            }
            catch (Exception ex)
            {
                //Referral links should remain accurate. Let's blow up if we can't create one. We don't want to accidentally store anything.
                logger.LogError(ex, "An error occured while creating a deferred deep link for referral code {code}. Throwing", referralCode);
                throw;
            }

        }

        public async Task<DeferredLink?> ResolveDeepLink(string linkCode, CancellationToken cancellationToken)
        {
            try
            {
                (var isFound, var link) = await deepLinkClient.FetchDeferredLink(linkCode, cancellationToken);

                if (isFound)
                {
                    return link;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while resolving a deferred link for linkCode {linkCode}. Returning null", linkCode);
                return null;
            }
        }

    }
}
