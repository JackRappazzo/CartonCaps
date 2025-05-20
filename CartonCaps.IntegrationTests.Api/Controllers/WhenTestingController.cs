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
        
        //We can eventually add a Given that adds Bearer tokens as part of the process

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

        [Then]
        public void ShouldReturnBadRequest()
        {
            Assert.That(Response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }
    }
}
