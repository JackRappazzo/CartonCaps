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
using NSubstitute.Core.Arguments;

namespace CartonCaps.UnitTests.Services.ReferralAudit.UserReferralProcessorTests.UpdateReferralStatusTests
{
    public class GivenAllReferralsAreGood : WhenTestingUpdate
    {
        Guid ReferralIdOne = Guid.NewGuid();
        Guid ReferralIdTwo = Guid.NewGuid();

        IEnumerable<ReferredUser> Referrals;


        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIdIsSet)
            .And(UserRepositoryReturnsUser)
            .And(ReferralRepositoryReturnsReferrals)
            .And(IpEvaluatorReturnsEmpty)
            .And(SessionEvaluatorReturnsEmpty)
            .And(IndividualEvaluatorReturnsCompletedForAll)
            .When(UpdateIsCalled)
            .Then(ShouldUpdateReferrals);

        [Given]
        public void ReferralRepositoryReturnsReferrals()
        {

            Referrals = new []
            {
                new ReferredUser()
                {
                    CreatedOn = DateTime.Now,
                    ReferralState = ReferralState.Pending,
                    Id = ReferralIdOne,
                },
                new ReferredUser()
                {
                    CreatedOn = DateTime.Now,
                    ReferralState = ReferralState.Pending,
                    Id = ReferralIdTwo,
                }
            };

            ReferralRepository.GetReferredUsersByReferringId(UserId, CancellationToken)
                .Returns(Referrals);

        }

        [Given]
        public void IpEvaluatorReturnsEmpty()
        {
            IpThresholdEvaluator.GetReferralIdsThatExceedIpThreshold(UserIpAddress, Referrals, SameIpThreshold).
                Returns([]);
        }

        [Given]
        public void SessionEvaluatorReturnsEmpty()
        {
            SessionIdThresholdEvaluator.GetReferralIdsThatExceedSessionIdThreshold(UserSessionId, Referrals, SameIpThreshold).
                Returns([]);
        }

        [Given]
        public void IndividualEvaluatorReturnsCompletedForAll()
        {
            IndividualReferralStateEvaluator
                .EvaluateReferralState(
                    referral: Arg.Is<ReferredUser>(u => Referrals.Any(r => r.Id == u.Id)),
                    CancellationToken)
                .Returns(ReferralState.Completed);
        }

        [Then]
        public void ShouldUpdateReferrals()
        {
            ReferralRepository.Received(1).UpdateReferralStateById(ReferralIdOne, ReferralState.Completed, CancellationToken);
            ReferralRepository.Received(1).UpdateReferralStateById(ReferralIdTwo, ReferralState.Completed, CancellationToken);

        }
    }
}
