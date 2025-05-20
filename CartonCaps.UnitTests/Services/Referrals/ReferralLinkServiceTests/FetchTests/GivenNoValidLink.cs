using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CartonCaps.UnitTests.Services.Referrals.ReferralLinkServiceTests.FetchTests
{
    public class GivenNoValidLink : WhenTestingFetchValidLink
    {
        string ExpectedReferralUrl = "https://sample.com/app/abc123DE";

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIsSet)
            .And(RepositoryDoesNotReturnALink)
            .And(DeferredLinkServiceCanCreateLink)
            .When(FetchValidLinkIsCalled)
            .Then(ShouldCallInsertLink)
            .And(ShouldReturnExpectedLink);

        [Given]
        public void RepositoryDoesNotReturnALink()
        {
            ReferralLinkRepository.FetchUnexpiredReferralLinkByUserId(User.Id, CancellationToken)
                .ReturnsNull();
        }

        [Given]
        public void DeferredLinkServiceCanCreateLink()
        {
            DeferredLinkService.CreateReferralDeepLink(User.ReferralCode, CancellationToken)
                .Returns(ExpectedReferralUrl);
        }

        [Then]
        public void ShouldCallInsertLink()
        {
            ReferralLinkRepository
                .Received(1)
                .InsertReferralLink(
                    User.Id,
                    ExpectedReferralUrl,
                    expiration: Arg.Is<DateTime>(d => d > DateTime.Now + TimeSpan.FromDays(59)), //Leaving some leeway here
                                                                                                 //Need a better way to test expiration?
                    CancellationToken);
        }

        [Then]
        public void ShouldReturnExpectedLink()
        {
            Assert.That(Result, Is.EqualTo(ExpectedReferralUrl));
        }
    }
}
