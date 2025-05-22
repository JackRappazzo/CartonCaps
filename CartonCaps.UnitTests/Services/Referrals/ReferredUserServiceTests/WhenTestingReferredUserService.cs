using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Repositories;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Composable;
using Microsoft.Extensions.Logging;

namespace CartonCaps.UnitTests.Services.Referrals.ReferralServiceTests
{
    public abstract class WhenTestingReferredUserService : ComposableTestingTheBehaviourOf
    {
        [ItemUnderTest]
        protected ReferredUserService ReferredUserService;

        [Dependency]
        protected IReferralRepository ReferralRepository;

        [Dependency]
        protected ILogger<ReferredUserService> Logger;

        protected CancellationToken CancellationToken = default;
    }
}
