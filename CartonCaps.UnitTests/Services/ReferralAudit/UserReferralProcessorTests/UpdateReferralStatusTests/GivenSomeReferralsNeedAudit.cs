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
    public class GivenSomeReferralsNeedAudit : WhenTestingUpdate
    {
        IEnumerable<ReferredUser> Referrals;

        Guid ExpectedAuditGuidOne = Guid.NewGuid();
        Guid ExpectedAuditGuidTwo = Guid.NewGuid();

        Guid ExpectedCompleteGuidOne = Guid.NewGuid();

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(UserIdIsSet)
            .And(UserRepositoryReturnsUser)
            .And(ReferralRepositoryReturnsReferrals)
            .And(IpThresholdFailsOne)
            .And(SessionThresholdFailsOne)
            .And(IndividualEvaluatorPassesOne)
            .When(UpdateIsCalled)
            .Then(ShouldSetTwoToNeedsAudit)
            .And(ShouldSetOneToCompleted);

        [Given]
        public void ReferralRepositoryReturnsReferrals()
        {
            Referrals = new[]
            {
                new ReferredUser()
                {
                    Id = ExpectedAuditGuidOne,
                    ReferralState = ReferralState.Pending,
                    CreatedOn = DateTime.Now,
                },
                new ReferredUser()
                {
                    Id = ExpectedAuditGuidTwo,
                    ReferralState = ReferralState.Pending,
                    CreatedOn = DateTime.Now,
                },
                new ReferredUser()
                {
                    Id = ExpectedCompleteGuidOne,
                    ReferralState = ReferralState.Pending,
                    CreatedOn = DateTime.Now,
                }
            };

            ReferralRepository.GetReferredUsersByReferringId(UserId, CancellationToken)
                .Returns(Referrals);

        }

        [Given]
        public void IpThresholdFailsOne()
        {
            IpThresholdEvaluator.GetReferralIdsThatExceedIpThreshold(UserIpAddress, Referrals, SameIpThreshold)
                .Returns(new[] { ExpectedAuditGuidOne });
        }

        [Given]
        public void SessionThresholdFailsOne()
        {
            SessionIdThresholdEvaluator.GetReferralIdsThatExceedSessionIdThreshold(UserSessionId, Referrals, SameSessionIdThreshold)
                .Returns(new[] { ExpectedAuditGuidTwo });
        }

        [Given]
        public void IndividualEvaluatorPassesOne()
        {
            IndividualReferralStateEvaluator.EvaluateReferralState(Arg.Is<ReferredUser>(r=>r.Id == ExpectedCompleteGuidOne), CancellationToken)
                .Returns(ReferralState.Completed);
        }

        [Then]
        public void ShouldSetTwoToNeedsAudit()
        {
            ReferralRepository.Received(1).UpdateReferralStateById(ExpectedAuditGuidOne, ReferralState.NeedsAudit, CancellationToken);
            ReferralRepository.Received(1).UpdateReferralStateById(ExpectedAuditGuidTwo, ReferralState.NeedsAudit, CancellationToken);
        }

        [Then]
        public void ShouldSetOneToCompleted()
        {
            ReferralRepository.Received(1).UpdateReferralStateById(ExpectedCompleteGuidOne, ReferralState.Completed, CancellationToken);
        }
    }
}
