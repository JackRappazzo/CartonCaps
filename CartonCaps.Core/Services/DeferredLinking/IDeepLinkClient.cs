using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Core.Services.DeferredDeepLinking
{
    public interface IDeepLinkClient
    {
        Task<string> CreateDeepLink(string destination, Dictionary<string, object> metaData, CancellationToken cancellationToken);
        Task<(bool linkFound, DeferredLink? link)> FetchDeferredLink(string deferredLinkCode, CancellationToken cancellationToken);
    }
}
