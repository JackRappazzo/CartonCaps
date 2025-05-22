using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Composable;
using Microsoft.Extensions.Logging;

namespace CartonCaps.UnitTests.Services.DeferredLinking.DeferredLinkServiceTests
{
    public abstract class WhenTestingDeferredLinkService : ComposableTestingTheBehaviourOf
    {
        [ItemUnderTest]
        protected DeferredLinkService DeferredLinkService;

        [Dependency]
        protected IDeepLinkClient DeepLinkClient;

        [Dependency]
        protected ILogger<DeferredLinkService> Logger;

        protected CancellationToken CancellationToken = default;

    }
}
