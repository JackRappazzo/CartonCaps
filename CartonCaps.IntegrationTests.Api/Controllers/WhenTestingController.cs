using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Attributes;

namespace CartonCaps.IntegrationTests.Api.Controllers
{
    public abstract class WhenTestingController : WhenTestingWithApi
    {
        protected HttpResponseMessage Response;

        [When]
        public async Task GetIsCalled()
        {
            Response = await Client.GetAsync(RequestUrl);
        }

        [Then]
        public void ShouldReturnOk()
        {
            Assert.That(Response.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
