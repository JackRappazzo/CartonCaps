using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using LeapingGorilla.Testing.Core.Attributes;

namespace CartonCaps.UnitTests.Services.DeferredLinking.DeferredLinkServiceTests.CreateTests
{
    public abstract class WhenTestingCreateReferral : WhenTestingDeferredLinkService
    {
        protected string Result;
        protected string ReferralCode;

        [When]
        public async Task CreateReferralIsCalled()
        {
            Result = await DeferredLinkService.CreateReferralDeepLink(ReferralCode, CancellationToken);
        }
    }
}
