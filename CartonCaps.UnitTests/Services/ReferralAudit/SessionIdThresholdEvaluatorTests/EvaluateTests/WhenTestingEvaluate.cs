using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;

namespace CartonCaps.UnitTests.Services.ReferralAudit.SessionIdThresholdEvaluatorTests.EvaluateTests
{
    public abstract class WhenTestingEvaluate : WhenTestingSessionIdThresholdEvaluator
    {
        protected IEnumerable<Guid> Result;
        protected Guid SessionId;
        protected IEnumerable<ReferredUser> ReferredUsers;
        protected int Threshold;

        [Given]
        public void SessionIdIsSet()
        {
            SessionId = Guid.NewGuid();
        }

        [Given]
        public void ThresholdIsOne()
        {
            Threshold = 1;
        }

        [When]
        public void GetExceededIsCalled()
        {
            Result = SessionIdThresholdEvaluator.GetReferralIdsThatExceedSessionIdThreshold(SessionId, ReferredUsers, Threshold);
        }
    }
}
