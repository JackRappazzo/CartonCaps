using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Persistence.Models
{
    public class ReferralLink
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Url { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
