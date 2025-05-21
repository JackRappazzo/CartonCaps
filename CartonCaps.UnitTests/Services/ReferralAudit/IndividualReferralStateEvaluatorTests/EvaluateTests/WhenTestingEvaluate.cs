using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using CartonCaps.ReferralAudit.Core.Services;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Attributes;
using NSubstitute;

namespace CartonCaps.UnitTests.Services.ReferralAudit.IndividualReferralStateEvaluatorTests.EvaluateTests
{
    public abstract class WhenTestingEvaluate : WhenTestingIndividualReferrerStateEvaluator
    {
        protected ReferralState Result;

        protected ReferredUser ReferredUser;

        protected CartonCapsUser User;
        protected PurchaseHistory PurchaseHistory;


        [Given]
        public void ReferralIsSet()
        {
            ReferredUser = new ReferredUser()
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now - TimeSpan.FromDays(10),
                ReferredUserId = Guid.NewGuid()
            };
        }

        [Given]
        public void UserMeetsLoginCriteria()
        {
            User = new CartonCapsUser()
            {
                Id = ReferredUser.ReferredUserId,
                LastLoggedInOn = DateTime.Now,
            };
        }

        [Given]
        public void UserRepositoryReturnsReferredUser()
        {
            UserRepository
                .FetchUserById(ReferredUser.ReferredUserId, CancellationToken)
                .Returns(User);
        }

        [Given]
        public void UserPurchaseHistoryMeetsCriteria()
        {
            PurchaseHistory = new PurchaseHistory()
            {
                UserId = User.Id,
                AmountSpentUsd = 50d,
                CreatedOn = DateTime.Now - TimeSpan.FromDays(1)
            };

        }

        [Given]
        public void PurchaseHistoryRepositoryReturnsHistory()
        {
            PurchaseHistoryRepository
                .GetPurchaseHistoryByUserId(ReferredUser.ReferredUserId, CancellationToken)
                .Returns(PurchaseHistory);
        }


        [When]
        public async Task EvaluateIsCalled()
        {
            Result = await IndividualReferralStateEvaluator.EvaluateReferralState(ReferredUser, CancellationToken);
        }

        [Then]
        public void ShouldReturnPending()
        {
            Assert.That(Result, Is.EqualTo(ReferralState.Pending));
        }

    }
}
