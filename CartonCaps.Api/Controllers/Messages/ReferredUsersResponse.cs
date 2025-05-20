using CartonCaps.Persistence.Models;
using Newtonsoft.Json;

namespace CartonCaps.Api.Controllers.Messages
{
    [JsonObject]
    public class ReferredUsersResponse : PagedResponse<ReferredUser>
    {

        [JsonProperty("refferedUsers")]
        public override IEnumerable<ReferredUser> Items { get => base.Items; set => base.Items = value; }

    }
}
