using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;
using NSubstitute;
namespace CartonCaps.UnitTests.Services.Referrals.ReferredUserServiceTests.GetReferralsTests
{
    public class GivenReferralsExist : WhenTestingGetReferrals
    {

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIdIsSet)
            .And(SkipIsZero)
            .And(TakeIsTen)
            .And(RepositoryReturnsFiveReferrals)
            .When(GetReferralsIsCalled)
            .Then(ShouldReturnExpectedData)
            .And(ShouldReturnTotalOfFive)
            .And(ShouldSortCompletedFirst);

        [Given]
        public void SkipIsZero()
        {
            Skip = 0;
        }

        [Given]
        public void TakeIsTen()
        {
            Take = 10;
        }

        [Then]
        public void ShouldReturnTotalOfFive()
        {
            Assert.That(Result.Total, Is.EqualTo(5));
        }

        [Then]
        public void ShouldReturnExpectedData()
        {
            Assert.That(Result.ReferredUsers.Count(), Is.EqualTo(5));
        }

        [Then]
        public void ShouldSortCompletedFirst()
        {
            Assert.That(Result.ReferredUsers, Is.Ordered.By("ReferralState").Then.By("CreatedOn"));
        }

        [Then]
        public void ShouldNotReturnDenied()
        {
            Assert.That(Result.ReferredUsers.Where(r=>r.ReferralState == ReferralState.Denied).Count(), Is.EqualTo(0));
        }

    }
}
