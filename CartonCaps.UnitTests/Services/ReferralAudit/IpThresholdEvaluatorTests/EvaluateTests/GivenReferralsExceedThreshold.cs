using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Models;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit.Attributes;

namespace CartonCaps.UnitTests.Services.ReferralAudit.IpThresholdEvaluatorTests.EvaluateTests
{
    public class GivenReferralsExceedThreshold : WhenTestingEvaluate
    {

        Guid ExpectedGuidOne = Guid.NewGuid();
        Guid ExpectedGuidTwo = Guid.NewGuid();


        protected override ComposedTest ComposeTest() => TestComposer
            .Given(IpAddressIsSet)
            .And(ReferralsExceedThresholdForSameIp)
            .And(ThresholdIsOne)
            .When(GetExceededIsCalled)
            .Then(ShouldReturnOnlyTwoGuids)
            .And(ShouldReturnExpectedGuids);

     

        [Given]
        public void ReferralsExceedThresholdForSameIp()
        {

            ReferredUsers = new[]
            {
                new ReferredUser()
                {
                    Id = ExpectedGuidOne,
                    ReferredIpAddress = IpAddress,
                    ReferredSessionId = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                },
                new ReferredUser()
                {
                    Id = ExpectedGuidTwo,
                    ReferredIpAddress = IpAddress,
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
        public void ShouldReturnExpectedGuids()
        {
            var expectedGuids = new[] { ExpectedGuidOne, ExpectedGuidTwo };
            Assert.That(Result, Is.EquivalentTo(expectedGuids));
        }

        [Then]
        public void ShouldReturnOnlyTwoGuids()
        {
            Assert.That(Result.Count(), Is.EqualTo(2));
        }
    }
}
