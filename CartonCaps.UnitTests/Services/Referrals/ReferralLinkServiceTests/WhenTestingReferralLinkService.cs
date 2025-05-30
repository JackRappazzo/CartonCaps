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
using Microsoft.Extensions.Logging;

namespace CartonCaps.UnitTests.Services.Referrals.ReferralLinkServiceTests
{
    public abstract class WhenTestingReferralLinkService : ComposableTestingTheBehaviourOf
    {
        [ItemUnderTest]
        protected ReferralLinkService ReferralLinkService;
        
        [Dependency]
        protected IReferralLinkRepository ReferralLinkRepository;

        [Dependency]
        protected IDeferredLinkService DeferredLinkService;

        [Dependency]
        protected IUserRepository UserRepository;

        [Dependency]
        protected ILogger<ReferralLinkService> Logger;

        protected CancellationToken CancellationToken = default;

    }
}
