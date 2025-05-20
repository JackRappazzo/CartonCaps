using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CartonCaps.IntegrationTests.Api.Controllers.DeferredLinkControllerTests.ResolveTests
{
    public class GivenBadLinkCode : WhenTestingResolve
    {
        protected override ComposedTest ComposeTest() => TestComposer
            .Given(ApplicationIsRunning)
            .And(RequestUrlIsSetWithLinkCode)
            .And(DeferredLinkServiceCannotFindLink)
            .When(GetIsCalled)
            .Then(ShouldReturnNotFound);

        [Given]
        public void DeferredLinkServiceCannotFindLink()
        {
            DeferredLinkService.ResolveDeepLink(LinkCode, Arg.Any<CancellationToken>())
                .ReturnsNull();
        }
    }
}
