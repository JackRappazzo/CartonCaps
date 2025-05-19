using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.UnitTests.Services.DeferredLinking.DeferredLinkServiceTests.CreateTests;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using NSubstitute;

namespace CartonCaps.UnitTests.Services.DeferredLinking.DeferredLinkServiceTests.CreateReferralTests
{
    public class GivenHappyPath : WhenTestingCreateReferral
    {
        string ExpectedLink = "https://sample.com/app/link";


        protected override ComposedTest ComposeTest() => TestComposer
            .Given(ReferralCodeIsSet)
            .And(LinkClientReturnsExpectedUrl)
            .When(CreateReferralIsCalled)
            .Then(ShouldCallCreateDeepLink)
            .And(ShouldReturnExpectedLink);

        [Given]
        public void ReferralCodeIsSet()
        {
            ReferralCode = "samplecode";
        }

        [Given]
        public void LinkClientReturnsExpectedUrl()
        {
            DeepLinkClient.CreateDeepLink(
                DeferredLinkDestinations.ReferralRegistration,
                Arg.Is<Dictionary<string, object>>(data => (data["ReferralCode"] as string) == ReferralCode ),
                CancellationToken)
                .Returns(ExpectedLink);
        }

        [Then]
        public void ShouldReturnExpectedLink()
        {
            Assert.That(Result, Is.EqualTo(ExpectedLink));
        }

        [Then]
        public void ShouldCallCreateDeepLink()
        {
            DeepLinkClient.Received(1).CreateDeepLink(DeferredLinkDestinations.ReferralRegistration,
                Arg.Is<Dictionary<string, object>>(data => (data["ReferralCode"] as string) == ReferralCode),
                CancellationToken);
        }
    }
}
