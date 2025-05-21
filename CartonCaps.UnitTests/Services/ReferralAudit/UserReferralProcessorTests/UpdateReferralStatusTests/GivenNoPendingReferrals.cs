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

namespace CartonCaps.UnitTests.Services.ReferralAudit.UserReferralProcessorTests.UpdateReferralStatusTests
{
    public class GivenNoPendingReferrals : WhenTestingUpdate
    {
        IEnumerable<ReferredUser> Referrals;

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIdIsSet)
            .And(UserRepositoryReturnsUser)
            .And(ReferralRepositoryReturnsOnlyCompleted)
            .When(UpdateIsCalled)
            .Then(ShouldNotEvaluateIpAddresses)
            .And(ShouldNotEvaluateSessions)
            .And(ShouldNotUpdateReferralStates);


        [Given]
        public void ReferralRepositoryReturnsOnlyCompleted()
        {
            Referrals = new[] {
                new ReferredUser()
                {
                    ReferredUserId = Guid.NewGuid(),
                    ReferringUserId = UserId,
                    CreatedOn = DateTime.Now,
                    ReferralState = ReferralState.Completed,
                },
                 new ReferredUser()
                {
                    ReferredUserId = Guid.NewGuid(),
                    ReferringUserId = UserId,
                    CreatedOn = DateTime.Now,
                    ReferralState = ReferralState.Completed,
                }
            };

            ReferralRepository.GetReferredUsersByReferringId(UserId, CancellationToken)
                .Returns(Referrals);
        }

        [Then]
        public void ShouldNotUpdateReferralStates()
        {
            ReferralRepository
                .DidNotReceiveWithAnyArgs()
                .UpdateReferralStateById(Arg.Any<Guid>(), Arg.Any<ReferralState>(), Arg.Any<CancellationToken>());
        }

        [Then]
        public void ShouldNotEvaluateIpAddresses()
        {
            IpThresholdEvaluator
                .DidNotReceiveWithAnyArgs()
                .GetReferralIdsThatExceedIpThreshold(Arg.Any<string>(), Arg.Any<IEnumerable<ReferredUser>>(), Arg.Any<int>());
        }

        [Then]
        public void ShouldNotEvaluateSessions()
        {
            SessionIdThresholdEvaluator
                .DidNotReceiveWithAnyArgs()
                .GetReferralIdsThatExceedSessionIdThreshold(Arg.Any<Guid>(), Arg.Any<IEnumerable<ReferredUser>>(), Arg.Any<int>());
        }
    }
}
