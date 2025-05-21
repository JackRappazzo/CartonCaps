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
    public class GivenFailsPurchaseCriteria : WhenTestingEvaluate
    {
        protected override ComposedTest ComposeTest() => TestComposer
             .Given(ReferralIsSet)
            .And(UserMeetsLoginCriteria)
            .And(UserPurchaseHistoryFailsCriteria)
            .And(UserRepositoryReturnsReferredUser)
            .And(PurchaseHistoryRepositoryReturnsHistory)
            .When(EvaluateIsCalled)
            .Then(ShouldReturnPending);

        [Given]
        public void UserPurchaseHistoryFailsCriteria()
        {
            PurchaseHistory = new PurchaseHistory()
            {
                UserId = User.Id,
                AmountSpentUsd = 0d,
                CreatedOn = DateTime.Now,
            };
        }
    }
}
