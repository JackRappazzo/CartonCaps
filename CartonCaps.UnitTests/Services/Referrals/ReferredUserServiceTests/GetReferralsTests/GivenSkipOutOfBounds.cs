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
    public class GivenSkipOutOfBounds : WhenTestingGetReferrals
    {
        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIdIsSet)
            .And(SkipIsGreaterThanCollection)
            .And(TakeIsSet)
            .And(RepositoryReturnsFiveReferrals)
            .When(GetReferralsIsCalled)
            .Then(ShouldSetTotalToFive)
            .And(ShouldReturnEmptySet);


        [Given]
        public void SkipIsGreaterThanCollection()
        {
            Skip = 50;
        }

        [Given]
        public void TakeIsSet()
        {
            Take = 5;
        }


        [Then]
        public void ShouldReturnEmptySet()
        {
            Assert.That(Result.ReferredUsers.Count, Is.EqualTo(0));
        }
    }
}
