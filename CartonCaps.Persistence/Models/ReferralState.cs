using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Persistence.Models
{

    /// <summary>
    /// Describes the acceptance state a referral is in
    /// </summary>
    public enum ReferralState
    {
        /// <summary>
        /// The referral is considered good and the user can receive credit
        /// </summary>
        Completed = 0, 
        
        /// <summary>
        /// The referral has been created but hasn't met the criteria for completion yet
        /// </summary>
        Pending = 1, 
        
        /// <summary>
        /// The referral needs to be reviewed by an auditing process
        /// </summary>
        NeedsAudit = 2, 
        
        /// <summary>
        /// The referral has been failed by an audit process or other criteria
        /// </summary>
        Denied = 3
    };
}
