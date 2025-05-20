using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Persistence.Models
{
    public class ReferralCodeModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public string ReferralCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
