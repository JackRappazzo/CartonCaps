using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Persistence.Models
{
    /// <summary>
    /// Placeholder model for the purchase history of a given user.
    /// Final model would likely be granular and per-purchase.
    /// This is just to mock an audit process
    /// </summary>
    public class PurchaseHistory
    {
        /// <summary>
        /// User making the purchase
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Amount spent, in US Dollars
        /// </summary>
        public double AmountSpentUsd { get; set; }

        /// <summary>
        /// Date this record was created
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
