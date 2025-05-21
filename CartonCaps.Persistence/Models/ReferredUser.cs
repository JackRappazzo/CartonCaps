using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Persistence.Models
{
    /// <summary>
    /// Represents a user that has been referred by another
    /// </summary>
    public class ReferredUser
    {
        /// <summary>
        /// ID that represents the user that made the referral
        /// </summary>
        public Guid ReferringUserId { get; set; }

        /// <summary>
        /// Shortened version of the user's name, for display in the referral page
        /// Takes the format First L. (John D. or Sally M.)
        /// </summary>
        public string TruncatedName { get; set; }

        /// <summary>
        /// The state the referral is in
        /// </summary>
        public ReferralState ReferralState { get; set; }
        
        /// <summary>
        /// The date the referral was created
        /// </summary>
        public DateTime CreatedOn { get; set; }

    }
}
