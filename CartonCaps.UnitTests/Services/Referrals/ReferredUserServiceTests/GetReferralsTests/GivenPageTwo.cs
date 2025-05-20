using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;

namespace CartonCaps.UnitTests.Services.Referrals.ReferredUserServiceTests.GetReferralsTests
{
    public class GivenPageTwo : WhenTestingGetReferrals
    {
        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIdIsSet)
            .And(SkipIsTwo)
            .And(TakeIsTwo)
            .And(RepositoryReturnsFiveReferrals)
            .When(GetReferralsIsCalled)
            .Then(ShouldReturnTwoResults)
            .And(ShouldSkipFirstTwoResults)
            .And(ShouldSetTotalToFive);

        [Given]
        public void TakeIsTwo()
        {
            Take = 2;
        }

        [Given]
        public void SkipIsTwo()
        {
            Skip = 2;
        }

        [Then]
        public void ShouldReturnTwoResults()
        {
            Assert.That(Result.ReferredUsers.Count, Is.EqualTo(2));
        }

        [Then]
        public void ShouldSkipFirstTwoResults()
        {
            var expectedReferrals = GetFiveReferrals()
                .Skip(2)
                .Take(2)
                .OrderBy(r => r.ReferralState)
                .ThenBy(r => r.CreatedOn)
                .Select(r => r.TruncatedName); //Lets simplify what we're testing right now

            Assert.That(Result.ReferredUsers.Select(r=>r.TruncatedName), Is.EquivalentTo(expectedReferrals));
        }
    }
}
