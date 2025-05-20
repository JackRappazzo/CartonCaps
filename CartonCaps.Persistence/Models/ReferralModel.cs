using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Persistence.Models
{
    public class ReferralModel
    {
        public Guid UserId { get; set; }
        public Guid ReferredUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ReferralCodeUsed { get; set; }
    }
}
