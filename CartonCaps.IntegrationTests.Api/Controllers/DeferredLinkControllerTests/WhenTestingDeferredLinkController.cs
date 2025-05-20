using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Composable;
using Microsoft.Extensions.DependencyInjection;

namespace CartonCaps.IntegrationTests.Api.Controllers.DeferredLinkControllerTests
{
    public abstract class WhenTestingDeferredLinkController : WhenTestingController
    {
        [Mock] protected IDeferredLinkService DeferredLinkService;

        protected override void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient(_ =>  DeferredLinkService);

            base.InjectDependencies(services);
        }
    }
}
