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

namespace CartonCaps.UnitTests.Services.Referrals.ReferralLinkServiceTests.FetchTests
{
    public class GivenValidLinkExists : WhenTestingFetchValidLink
    {
        string ExpectedReferralUrl = "https://sample.com/app/link/abc123fg?referral_code=abc123fg";

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIdIsSet)
            .And(RepositoryReturnsExistingReferralLink)
            .When(FetchValidLinkIsCalled)
            .Then(ShouldNotCreateReferral)
            .And(ShouldNotInsertReferral)
            .And(ShouldReturnExpectedUrl)
            .And(ShouldReturnExpectedReferralCode);

        [Given]
        public void RepositoryReturnsExistingReferralLink()
        {
            ReferralLinkRepository.FetchUnexpiredReferralLinkByUserId(UserId, CancellationToken)
                .Returns(new ReferralLink()
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    CreatedOn = DateTime.Now - TimeSpan.FromDays(5),
                    ExpiresOn = DateTime.Now + TimeSpan.FromDays(55),
                    Url = ExpectedReferralUrl
                });
        }

        [Then]
        public void ShouldNotCreateReferral()
        {
            DeferredLinkService
                .DidNotReceiveWithAnyArgs()
                .CreateReferralDeepLink(Arg.Any<string>(), Arg.Any<CancellationToken>());
        }

        [Then]
        public void ShouldNotInsertReferral()
        {
            ReferralLinkRepository
                .DidNotReceiveWithAnyArgs()
                .InsertReferralLink(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<CancellationToken>());
        }

        [Then]
        public void ShouldReturnExpectedUrl()
        {
            Assert.That(Result.deferredLink, Is.EqualTo(ExpectedReferralUrl));
        }

        [Then]
        public void ShouldReturnExpectedReferralCode()
        {
            Assert.That(Result.referralCode, Is.EqualTo("abc123fg"));
        }
    }
}
