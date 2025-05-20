using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Core.Services.DeferredDeepLinking
{
    /// <inheritdoc cref="IDeferredLinkService" />
    public class DeferredLinkService : IDeferredLinkService
    {
        private IDeepLinkClient deepLinkClient;

        public DeferredLinkService(IDeepLinkClient deepLinkClient)
        {
            this.deepLinkClient = deepLinkClient;
        }

        public async Task<string> CreateReferralDeepLink(string referralCode, CancellationToken cancellationToken)
        {
            var metaData = new Dictionary<string, object>();
            metaData.Add("ReferralCode", referralCode);

            var url = await deepLinkClient.CreateDeepLink(DeferredLinkDestinations.ReferralRegistration, metaData, cancellationToken);

            return url;
        }

        public async Task<DeferredLink?> ResolveDeepLink(string linkCode, CancellationToken cancellationToken)
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

    }
}
