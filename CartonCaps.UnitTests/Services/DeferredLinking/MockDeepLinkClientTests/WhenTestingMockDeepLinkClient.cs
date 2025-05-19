using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredLinking;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Composable;

namespace CartonCaps.UnitTests.Services.DeferredLinking.MockDeepLinkClientTests
{
    public abstract class WhenTestingMockDeepLinkClient : ComposableTestingTheBehaviourOf
    {
        [ItemUnderTest]
        protected MockDeepLinkClient MockDeepLinkClient;

        [Dependency]
        protected IShortCodeGenerator ShortCodeGenerator;

        protected CancellationToken CancellationToken = default;
    }
}
