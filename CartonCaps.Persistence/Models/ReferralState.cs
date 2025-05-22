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
        Completed, 
        
        /// <summary>
        /// The referral has been created but hasn't met the criteria for completion yet
        /// </summary>
        Pending, 
        
        /// <summary>
        /// The referral needs to be reviewed by an auditing process
        /// The auditing process is not implemented in this example
        /// </summary>
        NeedsAudit, 
        
        /// <summary>
        /// The referral has been failed by an audit process or other criteria
        /// The auditing process is not implemented in this example
        /// </summary>
        Denied
    };
}
