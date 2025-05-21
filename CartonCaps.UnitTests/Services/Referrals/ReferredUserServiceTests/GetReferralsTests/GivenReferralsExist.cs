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
            .And(ShouldSetTotalToFive)
            .And(ShouldSortByStateThenCreated);

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
        public void ShouldReturnExpectedData()
        {
            Assert.That(Result.ReferredUsers.Count(), Is.EqualTo(5));

            var resultNames = Result.ReferredUsers
                .Select(r => r.TruncatedName);

            var expectedNames = GetStoredReferrals()
                .Where(r => r.ReferralState != ReferralState.Denied)
                .Select(r => r.TruncatedName);

            Assert.That(resultNames, Is.EquivalentTo(expectedNames));
        }

        [Then]
        public void ShouldSortByStateThenCreated()
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
