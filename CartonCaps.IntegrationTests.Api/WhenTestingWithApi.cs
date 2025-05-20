using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.Core.Composable;
using LeapingGorilla.Testing.NUnit;
using LeapingGorilla.Testing.NUnit.Composable;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CartonCaps.IntegrationTests.Api
{
    public abstract class WhenTestingWithApi : ComposableTestingTheBehaviourOf
    {
        protected HttpClient Client { get; private set; }

        protected WebApplicationFactory<Program> Factory;

        protected string RequestUrl;

        protected virtual void InjectDependencies(IServiceCollection services)
        {

        }

        [Given]
        protected void ApplicationIsRunning()
        {
            var directory = Directory.GetCurrentDirectory();
            Factory = new WebApplicationFactory<Program>();

            Client = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(collection => InjectDependencies(collection));
            }).CreateClient();
        }

        [TearDown]
        protected void TeardownDependencies()
        {
            Client.Dispose();
            Factory.Dispose();
        }


    }
}
