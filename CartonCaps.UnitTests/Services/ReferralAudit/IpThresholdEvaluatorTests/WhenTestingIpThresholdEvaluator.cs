using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.ReferralAudit.Core.Services;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Composable;

namespace CartonCaps.UnitTests.Services.ReferralAudit.IpThresholdEvaluatorTests
{
    public abstract class WhenTestingIpThresholdEvaluator : ComposableTestingTheBehaviourOf
    {
        [ItemUnderTest]
        protected IpThresholdEvaluator IpThresholdEvaluator { get; set; }
    }
}
