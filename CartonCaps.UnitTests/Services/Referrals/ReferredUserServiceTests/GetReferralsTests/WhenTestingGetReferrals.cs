using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Models;
using CartonCaps.UnitTests.Services.Referrals.ReferralServiceTests;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Attributes;
using NSubstitute;

namespace CartonCaps.UnitTests.Services.Referrals.ReferredUserServiceTests.GetReferralsTests
{
    public abstract class WhenTestingGetReferrals : WhenTestingReferredUserService
    {
        protected Guid UserId;

        protected int Skip;
        protected int Take;

        protected (int Total, IEnumerable<ReferredUser> ReferredUsers) Result;

        private IEnumerable<ReferredUser> StoredReferrals;

        [Given]
        public void UserIdIsSet()
        {
            UserId = Guid.NewGuid();
        }

        [Given]
        public void RepositoryReturnsFiveReferrals()
        {
            var data = GetStoredReferrals();

            ReferralRepository.GetReferredUsersByReferringId(UserId, CancellationToken)
                .Returns(data);

        }


        /// <summary>
        /// Generates five referrals and returns them.
        /// Will not generate new ones
        /// </summary>
        /// <remarks>We are doing it this way so that we don't generate new guids by accident 
        /// and potentially break an equivalency check</remarks>
        /// <returns></returns>
        protected IEnumerable<ReferredUser> GetStoredReferrals()
        {
            if (StoredReferrals == null)
            {
                StoredReferrals = new ReferredUser[] {
                    new ReferredUser()
                    {
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(1),
                        ReferredUserId = Guid.NewGuid(),
                        ReferralState = ReferralState.Completed,
                        ReferringUserId = UserId,
                        TruncatedName = "Truncated N",
                    },
                    new ReferredUser()
                    {
                        CreatedOn = DateTime.Now - TimeSpan.FromHours(5),
                        ReferredUserId = Guid.NewGuid(),
                        ReferralState = ReferralState.Completed,
                        ReferringUserId = UserId,
                        TruncatedName = "Mister E.",
                    },
                    new ReferredUser()
                    {
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(30),
                        ReferredUserId = Guid.NewGuid(),
                        ReferralState = ReferralState.Completed,
                        ReferringUserId = UserId,
                        TruncatedName = "Sam P."
                    },
                    new ReferredUser()
                    {
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(5),
                        ReferredUserId = Guid.NewGuid(),
                        ReferralState = ReferralState.Pending,
                        ReferringUserId = UserId,
                        TruncatedName = "Sara H."
                    },
                    new ReferredUser()
                    {
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(3),
                        ReferredUserId = Guid.NewGuid(),
                        ReferralState = ReferralState.Completed,
                        ReferringUserId = UserId,
                        TruncatedName = "Stephanie R."
                    },
                    new ReferredUser()
                    {
                        CreatedOn = DateTime.Now - TimeSpan.FromDays(3),
                        ReferredUserId = Guid.NewGuid(),
                        ReferralState = ReferralState.Denied,
                        ReferringUserId = UserId,
                        TruncatedName = "Bad A."
                    }
                };
            }

            return StoredReferrals;
        }

        [When]
        public async Task GetReferralsIsCalled()
        {
            Result = await ReferredUserService.GetUserReferralsById(UserId, Skip, Take, CancellationToken);
        }


        [Then]
        public void ShouldSetTotalToFive()
        {
            Assert.That(Result.Total, Is.EqualTo(5));
        }

    }
}
