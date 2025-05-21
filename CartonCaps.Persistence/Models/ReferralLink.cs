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

        /// <summary>
        /// ID of the user that owns this referral link
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The referral link itself
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Expiration date
        /// </summary>
        public DateTime ExpiresOn { get; set; }

        /// <summary>
        /// Date this referral was created
        /// </summary>
        public DateTime CreatedOn { get; set; }

    }
}
