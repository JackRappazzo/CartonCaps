using System.Text.Json.Nodes;
using CartonCaps.Api.Controllers.Messages;
using CartonCaps.Core.Services.DeferredDeepLinking;
using Microsoft.AspNetCore.Mvc;

namespace CartonCaps.Api.Controllers
{
    
    [ApiController]
    [Route("api/deferredLinks")]
    public class DeferredLinkController : ControllerBase
    {
        IDeferredLinkService deferredLinkService;

        public DeferredLinkController(IDeferredLinkService deferredLinkService)
        {
            this.deferredLinkService = deferredLinkService;
        }

        [HttpGet("resolve/{linkCode}")]
        public async Task<IActionResult> ResolveDeferredLink(string linkCode, CancellationToken cancellationToken)
        {
            var deferredLink = await deferredLinkService.ResolveDeepLink(linkCode, cancellationToken);
            
            if (deferredLink == null)
            {
                return NotFound("The requested deferred link does not exist");
            }
            else
            {
                var response = new DeferredLinkResponse()
                {
                    Destination = deferredLink.Destination,
                    Metadata = deferredLink.Data
                };

                return Ok(response);
            }
        }
    }
}
