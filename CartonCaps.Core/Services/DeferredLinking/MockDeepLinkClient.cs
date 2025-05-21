using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;

namespace CartonCaps.Core.Services.DeferredLinking
{

    /// <summary>
    /// Mock client for a fictional deferred deep link service
    /// </summary>
    public class MockDeepLinkClient : IDeepLinkClient
    {
        const int ShortCodeLength = 10;

        private static ConcurrentDictionary<string, DeferredLink?> links = new ConcurrentDictionary<string, DeferredLink?>();
       
        
        private IShortCodeGenerator codeGenerator;

        private const string baseUrl = "https://cartoncaps.link/{0}";

        public MockDeepLinkClient(IShortCodeGenerator shortCodeGenerator)
        {
            this.codeGenerator = shortCodeGenerator;
        }

        /// <summary>
        /// Creates a new deep link and stores it along with the provided metadata
        /// </summary>
        /// <param name="destination">The location in the app to direct the user to</param>
        /// <param name="metaData">Data associated with this link</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> CreateDeepLink(string destination, Dictionary<string, object> metaData, CancellationToken cancellationToken)
        {

            string code = string.Empty;
            GenerateAndReserveShortCode(ref code);
            links[code] = new DeferredLink() { Data = metaData, Destination = destination };

            return string.Format(baseUrl, code);

        }

        /// <summary>
        /// Retrieves a link by its code as well as any metadata
        /// </summary>
        /// <param name="deferredLinkCode">The unique code for the desired deferred link. This is typically found as part of the url. (https://sample.com/app/ABC123)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<(bool linkFound, DeferredLink? link)> FetchDeferredLink(string deferredLinkCode, CancellationToken cancellationToken)
        {
            var linkFound = links.TryGetValue(deferredLinkCode, out var deferredLink);
            if(!linkFound)
            {
                return (false, null);
            }
            else
            {
                return (true, deferredLink);
            }
        }

        /// <summary>
        /// Recursively attempt to reserve generated short codes until one is found that does not already exist
        /// </summary>
        /// <param name="code"></param>
        /// <param name="tryCount"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private bool GenerateAndReserveShortCode(ref string code, int tryCount=0)
        {
            //We should never have to throw.
            //If we do, something is wrong with the generator
            if (tryCount >= 10)
                throw new Exception("Failed to find a unique short code");
            
            code = codeGenerator.GenerateShortCode(ShortCodeLength);

            var success = links.TryAdd(code, null);
            if (success)
            {
                return true;
            }
            else
            {
                tryCount++;
                return GenerateAndReserveShortCode(ref code, tryCount);
            }
        }
    }
}
