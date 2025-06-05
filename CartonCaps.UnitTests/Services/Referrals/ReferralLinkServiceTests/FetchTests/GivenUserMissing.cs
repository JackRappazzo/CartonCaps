using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using NSubstitute.ReturnsExtensions;

namespace CartonCaps.UnitTests.Services.Referrals.ReferralLinkServiceTests.FetchTests
{
    public class GivenUserMissing : WhenTestingFetchValidLink
    {
        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIdIsSet)
            .And(RepositoryDoesNotReturnALink)
            .And(UserRepositoryDoesNotReturnReferralCode)
            .When(FetchValidLinkIsCalled)
            .Then(ShouldReturnEmpty);

        [Given]
        public void UserRepositoryDoesNotReturnReferralCode()
        {
            UserRepository.FetchUsersReferralCode(UserId, CancellationToken)
                .ReturnsNull();
        }

        [Then]
        public void ShouldReturnEmpty()
        {

            Assert.That(Result.referralCode, Is.Empty);
            Assert.That(Result.deferredLink, Is.Empty); 
        }
    }
}
