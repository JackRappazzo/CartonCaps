using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Repositories;
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
        [Mock] protected IReferralLinkService ReferralLinkService;
        [Mock] protected IUserRepository UserRepository;

        protected override void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient(_ => DeferredLinkService);
            services.AddTransient(_ => ReferredUserService);
            services.AddTransient(_ => ReferralLinkService);
            services.AddTransient(_ => UserRepository);

            base.InjectDependencies(services);
        }
    }
}
