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

namespace CartonCaps.UnitTests.Services.Referrals.ReferredUserServiceTests.GetReferralsTests
{
    public class GivenNoReferralsExist : WhenTestingGetReferrals
    {

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIdIsSet)
            .And(SkipIsSet)
            .And(TakeIsSet)
            .And(ReferralRepositoryReturnsNoResults)
            .When(GetReferralsIsCalled)
            .Then(ShouldSetTotalToZero)
            .And(ShouldReturnEmptySet);

        [Given]
        public void SkipIsSet()
        {
            Skip = 0;
        }

        [Given]
        public void TakeIsSet()
        {
            Take = 5;
        }

        [Given]
        public void ReferralRepositoryReturnsNoResults()
        {
            ReferralRepository.GetReferredUsersByReferringId(UserId, CancellationToken)
                .Returns(new List<ReferredUser>());
        }

        [Then]
        public void ShouldSetTotalToZero()
        {
            Assert.That(Result.Total, Is.Zero);
        }

        [Then]
        public void ShouldReturnEmptySet()
        {
            Assert.That(Result.ReferredUsers.Count(), Is.Zero);
        }
    }
}
