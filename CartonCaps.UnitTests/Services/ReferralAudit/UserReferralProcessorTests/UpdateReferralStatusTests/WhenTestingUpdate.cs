using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using CartonCaps.ReferralAudit.Core.Services;
using LeapingGorilla.Testing.Core.Attributes;
using NSubstitute;

namespace CartonCaps.UnitTests.Services.ReferralAudit.UserReferralProcessorTests.UpdateReferralStatusTests
{
    public abstract class WhenTestingUpdate : WhenTestingUserReferralProcessor
    {
        protected Guid UserId;

        protected string UserIpAddress = "127.0.0.1";
        protected Guid UserSessionId = Guid.NewGuid();

        [When]
        public async Task UpdateIsCalled()
        {
            await UserReferralProcessor.UpdateUsersReferrals(UserId, CancellationToken);
        }

        [Given]
        public void UserIdIsSet()
        {
            UserId = Guid.NewGuid();
        }

        [Given]
        public void UserRepositoryReturnsUser()
        {
            UserRepository.FetchUserById(UserId, CancellationToken)
                .Returns(new CartonCapsUser()
                {
                    Id = UserId,
                    RegisteredIpAddress = UserIpAddress,
                    RegisteredSessionId = UserSessionId
                });
        }
    }
}
