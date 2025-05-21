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

namespace CartonCaps.UnitTests.Services.ReferralAudit.IndividualReferralStateEvaluatorTests.EvaluateTests
{
    public class GivenValidReferral : WhenTestingEvaluate
    {



        protected override ComposedTest ComposeTest() => TestComposer
            .Given(ReferralIsSet)
            .And(UserMeetsLoginCriteria)
            .And(UserPurchaseHistoryMeetsCriteria)
            .And(UserRepositoryReturnsReferredUser)
            .And(PurchaseHistoryRepositoryReturnsHistory)
            .When(EvaluateIsCalled)
            .Then(ShouldReturnComplete);

      

        [Then]
        public void ShouldReturnComplete()
        {
            Assert.That(Result, Is.EqualTo(ReferralState.Completed));
        }
    }
}
