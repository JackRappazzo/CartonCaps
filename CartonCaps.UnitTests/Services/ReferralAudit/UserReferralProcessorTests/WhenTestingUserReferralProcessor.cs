using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Repositories;
using CartonCaps.ReferralAudit.Core.Services;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Composable;
using NSubstitute;

namespace CartonCaps.UnitTests.Services.ReferralAudit.UserReferralProcessorTests
{
    public abstract class WhenTestingUserReferralProcessor : ComposableTestingTheBehaviourOf
    {
        [ItemUnderTest] protected UserReferralProcessor UserReferralProcessor;

        [Dependency] protected IUserRepository UserRepository;
        [Dependency] protected IReferralRepository ReferralRepository;
        [Dependency] protected IIndividualReferralStateEvaluator IndividualReferralStateEvaluator;
        [Dependency] protected IIpThresholdEvaluator IpThresholdEvaluator;
        [Dependency] protected ISessionIdThresholdEvaluator SessionIdThresholdEvaluator;
        [Dependency] protected IAuditThresholdConfigurationFactory AuditThresholdConfigurationFactory;

        protected int SameIpThreshold = 2;
        protected int SameSessionIdThreshold = 2;

        protected CancellationToken CancellationToken = default;

        protected override void CreateManualDependencies()
        {

            //Constructors are fired before [Given], so we must do our threshold work here
            AuditThresholdConfigurationFactory = Substitute.For<IAuditThresholdConfigurationFactory>();
            AuditThresholdConfigurationFactory
               .Create()
               .Returns(
                   new AuditThresholdConfiguration()
                   {
                       LoginThreshold = TimeSpan.FromDays(1),
                       PurchaseThreadhold  = 10d,
                       SameIpThreshold = 2,
                       SameMacThreshold = 2,
                       SameSessionThreshold = 2,
                   });
        }

    }
}
