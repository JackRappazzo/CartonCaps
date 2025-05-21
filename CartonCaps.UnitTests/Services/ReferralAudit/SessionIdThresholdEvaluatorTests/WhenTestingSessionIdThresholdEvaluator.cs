using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.ReferralAudit.Core.Services;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Composable;

namespace CartonCaps.UnitTests.Services.ReferralAudit.SessionIdThresholdEvaluatorTests
{
    public abstract class WhenTestingSessionIdThresholdEvaluator: ComposableTestingTheBehaviourOf
    {
        [ItemUnderTest]
        protected SessionIdThresholdEvaluator SessionIdThresholdEvaluator { get; set; }
    }
}
