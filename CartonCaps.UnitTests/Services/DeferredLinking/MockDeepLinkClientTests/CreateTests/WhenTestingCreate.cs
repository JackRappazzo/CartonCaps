using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;

namespace CartonCaps.UnitTests.Services.DeferredLinking.MockDeepLinkClientTests.CreateTests
{
    public abstract class WhenTestingCreate : WhenTestingMockDeepLinkClient
    {
        protected string Result;

        protected string Destination;
        protected Dictionary<string, object> Metadata;

        [When]
        public async Task CreateIsCalled()
        {
            Result = await MockDeepLinkClient.CreateDeepLink(Destination, Metadata, CancellationToken);
        }
    }
}
