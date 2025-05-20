using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Api.Controllers.Messages;
using CartonCaps.Core.Services.DeferredDeepLinking;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using Newtonsoft.Json;
using NSubstitute;

namespace CartonCaps.IntegrationTests.Api.Controllers.DeferredLinkControllerTests.ResolveTests
{
    public class GivenHappyPath : WhenTestingResolve
    {
  
        protected override ComposedTest ComposeTest() => TestComposer
            .Given(ApplicationIsRunning)
            .And(RequestUrlIsSetWithLinkCode)
            .And(DeferredLinkServiceCanResolve)
            .When(GetIsCalled)
            .Then(ShouldReturnOk)
            .And(ShouldReturnDestination);


        [Given]
        public void DeferredLinkServiceCanResolve()
        {
            DeferredLinkService.ResolveDeepLink(LinkCode, Arg.Any<CancellationToken>())
                .Returns(new DeferredLink()
                {
                    Data = new Dictionary<string, object>(),
                    Destination = "sample-destination"
                });
        }

        [Then]
        public async Task ShouldReturnDestination()
        {
            var result = JsonConvert.DeserializeObject<DeferredLinkResponse>((await Response.Content.ReadAsStringAsync()));

            Assert.That(result.Destination, Is.EqualTo("sample-destination"));
            Assert.That(result.Metadata, Is.Not.Null);
        }
    }
}
