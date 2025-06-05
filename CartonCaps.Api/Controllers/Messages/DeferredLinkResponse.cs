using Newtonsoft.Json;

namespace CartonCaps.Api.Controllers.Messages
{
    [JsonObject]
    public class DeferredLinkResponse
    {
        public required string Destination {  get; set; }
        public required Dictionary<string, object> Metadata { get; set; }
    }
}
