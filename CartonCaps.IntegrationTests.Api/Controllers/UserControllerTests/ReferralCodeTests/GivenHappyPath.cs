using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Api.Controllers.Messages;
using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using Newtonsoft.Json;
using NSubstitute;

namespace CartonCaps.IntegrationTests.Api.Controllers.UserControllerTests.ReferralCodeTests
{
    public class GivenHappyPath : WhenTestingGetReferralCode
    {
        protected string ExpectedDeferredLink = "https://sample.com/abcd1234";

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(ApplicationIsRunning)
            .And(RequestUrlIsSet)
            .And(UserRepositoryFindsUser)
            .And(ReferralLinkServiceReturnsLink)
            .When(GetIsCalled)
            .Then(ShouldReturnOk)
            .And(ShouldReturnExpectedCode);

        [Given]
        public void RequestUrlIsSet()
        {
            RequestUrl = "api/users/referralCode";
        }

        [Given]
        public void UserRepositoryFindsUser()
        {
            UserRepository.FetchUserById(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(
                    new CartonCapsUser()
                    {
                        Id = Guid.NewGuid(),
                        ReferralCode = "123abcde",
                    });
        }

        [Given]
        public void ReferralLinkServiceReturnsLink()
        {
            ReferralLinkService.FetchValidReferralLink(Arg.Is<CartonCapsUser>(u=>u.ReferralCode == "123abcde"), Arg.Any<CancellationToken>())
                .Returns(ExpectedDeferredLink);
        }

        [Then]
        public async Task ShouldReturnExpectedCode()
        {
            var payload = JsonConvert.DeserializeObject<ReferralCodeAndLinkResponse>(await Response.Content.ReadAsStringAsync());
            Assert.That(payload.DeferredLink, Is.EqualTo(ExpectedDeferredLink));
        }
    }
}
