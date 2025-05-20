using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.Core.Services.Referrals;
using LeapingGorilla.Testing.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace CartonCaps.IntegrationTests.Api.Controllers.UserControllerTests
{
    public abstract class WhenTestingUserController : WhenTestingController
    {
        protected override string RequestUrl => "api/users/referralCode";
            
        [Mock] protected IDeferredLinkService DeferredLinkService;
        [Mock] protected IReferredUserService ReferredUserService;

        protected override void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient(_ => DeferredLinkService);
            services.AddTransient(_ => ReferredUserService);
            base.InjectDependencies(services);
        }
    }
}
