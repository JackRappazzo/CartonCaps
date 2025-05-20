using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Repositories;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Composable;

namespace CartonCaps.UnitTests.Services.Referrals.ReferralCodeServiceTests
{
    public abstract class WhenTestingReferralCodeService : ComposableTestingTheBehaviourOf
    {
        [ItemUnderTest]
        protected ReferralCodeService ReferralCodeService;
        
        [Dependency]
        protected IReferralLinkRepository ReferralLinkRepository;

        [Dependency]
        protected IDeferredLinkService DeferredLinkService;

        protected CancellationToken CancellationToken = default;

    }
}
