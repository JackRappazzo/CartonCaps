using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Models;
using CartonCaps.UnitTests.Services.Referrals.ReferralServiceTests;
using LeapingGorilla.Testing.Core.Attributes;
using NSubstitute;

namespace CartonCaps.UnitTests.Services.Referrals.ReferredUserServiceTests.GetReferralsTests
{
    public abstract class WhenTestingGetReferrals : WhenTestingReferredUserService
    {
        protected Guid UserId;

        protected int Skip;
        protected int Take;

        protected (int Total, IEnumerable<ReferredUser> ReferredUsers) Result;

        [Given]
        public void UserIdIsSet()
        {
            UserId = Guid.NewGuid();
        }

        [Given]
        public void RepositoryReturnsFiveReferrals()
        {
            var data = GetFiveReferrals();

            ReferralRepository.GetReferredUsersByReferringId(UserId, CancellationToken)
                .Returns(data);

        }

        protected ReferredUser[] GetFiveReferrals()
        {
            return new ReferredUser[] {
                new ReferredUser()
                {
                    CreatedOn = DateTime.Now - TimeSpan.FromDays(1),
                    ReferralState = ReferralState.Completed,
                    ReferringUserId = UserId,
                    TruncatedName = "Truncated N",
                },
                new ReferredUser()
                {
                    CreatedOn = DateTime.Now - TimeSpan.FromHours(5),
                    ReferralState = ReferralState.Completed,
                    ReferringUserId = UserId,
                    TruncatedName = "Mister E.",
                },
                new ReferredUser()
                {
                    CreatedOn = DateTime.Now - TimeSpan.FromDays(30),
                    ReferralState = ReferralState.Completed,
                    ReferringUserId = UserId,
                    TruncatedName = "Sam P."
                },
                new ReferredUser()
                {
                    CreatedOn = DateTime.Now - TimeSpan.FromDays(5),
                    ReferralState = ReferralState.Pending,
                    ReferringUserId = UserId,
                    TruncatedName = "Sara H."
                },
                new ReferredUser()
                {
                    CreatedOn = DateTime.Now - TimeSpan.FromDays(3),
                    ReferralState = ReferralState.Completed,
                    ReferringUserId = UserId,
                    TruncatedName = "Stephanie R."
                },
                new ReferredUser()
                {
                     CreatedOn = DateTime.Now - TimeSpan.FromDays(3),
                    ReferralState = ReferralState.Denied,
                    ReferringUserId = UserId,
                    TruncatedName = "Bad A."
                }
            };
        }

        [When]
        public async Task GetReferralsIsCalled()
        {
            Result = await ReferredUserService.GetUserReferralsById(UserId, Skip, Take, CancellationToken);
        }
    }
}
