using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;

namespace CartonCaps.UnitTests.Services.ReferralAudit.IpThresholdEvaluatorTests.EvaluateTests
{
    public abstract class WhenTestingEvaluate : WhenTestingIpThresholdEvaluator
    {
        protected IEnumerable<Guid> Result;
        protected string IpAddress;
        protected IEnumerable<ReferredUser> ReferredUsers;
        protected int Threshold;

        [Given]
        public void IpAddressIsSet()
        {
            IpAddress = "127.0.0.1";
        }

        [Given]
        public void ThresholdIsOne()
        {
            Threshold = 1;
        }

        [When]
        public void GetExceededIsCalled()
        {
            Result = IpThresholdEvaluator.GetReferralIdsThatExceedIpThreshold(IpAddress, ReferredUsers, Threshold);
        }
    }
}
