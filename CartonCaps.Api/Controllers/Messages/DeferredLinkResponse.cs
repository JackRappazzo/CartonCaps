using Newtonsoft.Json;

namespace CartonCaps.Api.Controllers.Messages
{
    [JsonObject]
    public class DeferredLinkResponse
    {
        public string Destination {  get; set; }
        public Dictionary<string, object> Metadata { get; set; }
    }
}
