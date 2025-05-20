using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;

namespace CartonCaps.IntegrationTests.Api.Controllers.DeferredLinkControllerTests.ResolveTests
{
    public abstract class WhenTestingResolve : WhenTestingDeferredLinkController
    {
        protected string LinkCode = "abc123de";

        [Given]
        public void RequestUrlIsSetWithLinkCode()
        {
            RequestUrl = $"api/deferredLinks/resolve/{LinkCode}";
        }

    }
}
