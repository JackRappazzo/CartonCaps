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
        string ExpectedReferralCode = "abc123DE";
        string OriginalDeferredLink = "https://sample.com/app/abc123DE";
        string ExpectedReferralUrl = "https://sample.com/app/abc123DE?referral_code=abc123DE";

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIdIsSet)
            .And(RepositoryDoesNotReturnALink)
            .And(UserRepositoryFetchesReferralCode)
            .And(DeferredLinkServiceCanCreateLink)
            .When(FetchValidLinkIsCalled)
            .Then(ShouldCallInsertLink)
            .And(ShouldReturnExpectedReferralCode)
            .And(ShouldReturnExpectedUrl);

        [Given]
        public void UserRepositoryFetchesReferralCode()
        {
            UserRepository.FetchUsersReferralCode(UserId, CancellationToken)
                .Returns(ExpectedReferralCode);
        }

        [Given]
        public void DeferredLinkServiceCanCreateLink()
        {
            DeferredLinkService.CreateReferralDeepLink(ExpectedReferralCode, CancellationToken)
                .Returns(OriginalDeferredLink);
        }

        [Then]
        public void ShouldCallInsertLink()
        {
            ReferralLinkRepository
                .Received(1)
                .InsertReferralLink(
                    UserId,
                    ExpectedReferralUrl,
                    expiration: Arg.Is<DateTime>(d => d > DateTime.Now + TimeSpan.FromDays(59)), //Leaving some leeway here
                                                                                                 //Need a better way to test expiration?
                    CancellationToken);
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
