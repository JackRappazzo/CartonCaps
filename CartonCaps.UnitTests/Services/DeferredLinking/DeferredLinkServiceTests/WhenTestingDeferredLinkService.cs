using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Composable;

namespace CartonCaps.UnitTests.Services.DeferredLinking.DeferredLinkServiceTests
{
    public abstract class WhenTestingDeferredLinkService : ComposableTestingTheBehaviourOf
    {
        [ItemUnderTest]
        protected DeferredLinkService DeferredLinkService;

        [Dependency]
        protected IDeepLinkClient DeepLinkClient;

        protected CancellationToken CancellationToken = default;

    }
}
