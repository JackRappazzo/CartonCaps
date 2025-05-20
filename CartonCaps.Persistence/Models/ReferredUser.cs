using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Persistence.Models
{
    public class ReferredUser
    {
        public Guid ReferringUserId { get; set; }
        public string TruncatedName { get; set; }
        public ReferralState ReferralState { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
