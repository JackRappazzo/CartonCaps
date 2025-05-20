using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Core.Services.DeferredDeepLinking
{
    /// <summary>
    /// A client to interact with a third party deferred deep link service
    /// </summary>
    public interface IDeepLinkClient
    {
        /// <summary>
        /// Sends a request to the service to create a deferred deep link with associated metadata
        /// </summary>
        /// <param name="destination">The destination the app should navigate to when resolving this deep link</param>
        /// <param name="metaData">The data associated with this deep link, to be retrieved when resolved</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> CreateDeepLink(string destination, Dictionary<string, object> metaData, CancellationToken cancellationToken);
        
        /// <summary>
        /// Fetches a deferred deep link from the service along with any associated metadata
        /// </summary>
        /// <param name="deferredLinkCode">The unique code that identifies the deferred link</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<(bool linkFound, DeferredLink? link)> FetchDeferredLink(string deferredLinkCode, CancellationToken cancellationToken);
    }
}
