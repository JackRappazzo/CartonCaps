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

namespace CartonCaps.UnitTests.Services.ReferralAudit.IndividualReferralStateEvaluatorTests
{
    public abstract class WhenTestingIndividualReferrerStateEvaluator : ComposableTestingTheBehaviourOf
    {
        [ItemUnderTest]
        protected IndividualReferralStateEvaluator IndividualReferralStateEvaluator { get; set; }

        [Dependency]
        protected IUserRepository UserRepository { get; set; }

        [Dependency]
        protected IPurchaseHistoryRepository PurchaseHistoryRepository { get; set; }

        [Dependency]
        protected IAuditThresholdConfigurationFactory AuditThresholdConfigurationFactory { get; set; }

        protected CancellationToken CancellationToken = default;


        protected double PurchaseThreshold = 10d;
        protected TimeSpan LoginThreshold = TimeSpan.FromDays(1);

        protected override void CreateManualDependencies()
        {
            AuditThresholdConfigurationFactory = Substitute.For<IAuditThresholdConfigurationFactory>();
            AuditThresholdConfigurationFactory
               .Create()
               .Returns(
                   new AuditThresholdConfiguration()
                   {
                       LoginThreshold = LoginThreshold,
                       PurchaseThreshold = PurchaseThreshold,
                       SameIpThreshold = 2,
                       SameMacThreshold = 2,
                       SameSessionThreshold = 2,
                   });
        }


    }
}
