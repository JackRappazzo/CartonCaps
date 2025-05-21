using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;

namespace CartonCaps.UnitTests.Services.ReferralAudit.IpThresholdEvaluatorTests.EvaluateTests
{
    public class GivenNoReferralsBelowThreshold : WhenTestingEvaluate
    {

        protected override ComposedTest ComposeTest() => TestComposer
            .Given(IpAddressIsSet)
            .And(ReferralsAllHaveUniqueAddresses)
            .And(ThresholdIsOne)
            .When(GetExceededIsCalled)
            .Then(ShouldReturnEmptySet);

        [Given]
        public void ReferralsAllHaveUniqueAddresses()
        {

            ReferredUsers = new[]
            {
                new ReferredUser()
                {
                    Id = Guid.NewGuid(),
                    ReferredIpAddress = "127.4.4.4",
                    ReferredSessionId = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                },
                new ReferredUser()
                {
                    Id = Guid.NewGuid(),
                    ReferredIpAddress = "127.3.3.3",
                    ReferredSessionId = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                },
                new ReferredUser()
                {
                    Id = Guid.NewGuid(),
                    ReferredIpAddress = "127.2.2.1",
                    ReferredSessionId = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                }
            };
        }

        [Then]
        public void ShouldReturnEmptySet()
        {
            Assert.That(Result, Is.Empty);
        }
    }
}
