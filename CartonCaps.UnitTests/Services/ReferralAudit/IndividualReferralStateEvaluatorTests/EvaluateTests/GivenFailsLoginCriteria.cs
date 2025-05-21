using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;

namespace CartonCaps.UnitTests.Services.ReferralAudit.IndividualReferralStateEvaluatorTests.EvaluateTests
{
    public class GivenFailsLoginCriteria : WhenTestingEvaluate
    {
        protected override ComposedTest ComposeTest() => TestComposer
            .Given(ReferralIsSet)
            .And(UserDoesNotMeetLoginCriteria)
            .And(UserPurchaseHistoryMeetsCriteria)
            .And(UserRepositoryReturnsReferredUser)
            .And(PurchaseHistoryRepositoryReturnsHistory)
            .When(EvaluateIsCalled)
            .Then(ShouldReturnPending);

        [Given]
        public void UserDoesNotMeetLoginCriteria()
        {
            User = new CartonCapsUser()
            {
                Id = ReferredUser.ReferredUserId,
                //Our user hasn't logged in since they created their account
                LastLoggedInOn = ReferredUser.CreatedOn + TimeSpan.FromMinutes(1),
                CreatedOn = ReferredUser.CreatedOn,
            };
        }


    }
}
