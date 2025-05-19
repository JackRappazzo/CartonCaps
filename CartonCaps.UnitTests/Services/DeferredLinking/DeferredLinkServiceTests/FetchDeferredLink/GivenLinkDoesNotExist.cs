using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CartonCaps.UnitTests.Services.DeferredLinking.DeferredLinkServiceTests.FetchDeferredLink
{
    public class GivenLinkDoesNotExist : WhenTestingResolveLink
    {
        protected override ComposedTest ComposeTest() => TestComposer
            .Given(LinkCodeIsSet)
            .And(LinkClientDoesNotFindLink)
            .When(ResolveLinkIsCalled)
            .Then(ShouldReturnNull)
            .And(ShouldNotThrow);

        [Given]
        public void LinkClientDoesNotFindLink()
        {
            DeepLinkClient.FetchDeferredLink(Arg.Any<string>(), CancellationToken)
                .Returns((false, null));
        }

        [Then]
        public void ShouldReturnNull()
        {
            Assert.That(Result, Is.Null);
        }

        [Then]
        public void ShouldNotThrow()
        {
            Assert.That(ThrownException, Is.Null);
        }
    }
}
