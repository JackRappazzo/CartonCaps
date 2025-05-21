using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using NSubstitute.ReturnsExtensions;

namespace CartonCaps.UnitTests.Services.ReferralAudit.IndividualReferralStateEvaluatorTests.EvaluateTests
{
    public class GivenNoPurchaseHistory : WhenTestingEvaluate
    {
        protected override ComposedTest ComposeTest() => TestComposer
             .Given(ReferralIsSet)
            .And(UserMeetsLoginCriteria)
            .And(UserHasNoPurchaseHistory)
            .And(UserRepositoryReturnsReferredUser)
            .When(EvaluateIsCalled)
            .Then(ShouldReturnPending);

        [Given]
        public void UserHasNoPurchaseHistory()
        {
            PurchaseHistoryRepository.GetPurchaseHistoryByUserId(User.Id, CancellationToken)
                .ReturnsNull();
        }
    }
}
