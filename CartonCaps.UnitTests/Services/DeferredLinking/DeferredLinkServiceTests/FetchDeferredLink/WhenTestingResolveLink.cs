using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using LeapingGorilla.Testing.Core.Attributes;

namespace CartonCaps.UnitTests.Services.DeferredLinking.DeferredLinkServiceTests.FetchDeferredLink
{
    public abstract class WhenTestingResolveLink : WhenTestingDeferredLinkService
    {
        protected DeferredLink Result;

        protected string LinkCode;


        [Given]
        public void LinkCodeIsSet()
        {
            LinkCode = "ABC123DE";
        }

        [When]
        public async Task ResolveLinkIsCalled()
        {
            Result = await DeferredLinkService.ResolveDeepLink(LinkCode, CancellationToken);
        }
    }
}
