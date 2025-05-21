using CartonCaps.Persistence.Models;
using Newtonsoft.Json;

namespace CartonCaps.Api.Controllers.Messages
{
    /// <summary>
    /// Paged response containing referred users
    /// </summary>
    [JsonObject]
    public class ReferredUsersResponse : PagedResponse<ReferredUser>
    {

        /// <summary>
        /// Collection of users for display in the referred users page
        /// </summary>
        [JsonProperty("refferedUsers")]
        public override IEnumerable<ReferredUser> Items { get => base.Items; set => base.Items = value; }

    }
}
