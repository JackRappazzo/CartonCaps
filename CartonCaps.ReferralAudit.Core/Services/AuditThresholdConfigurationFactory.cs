using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.ReferralAudit.Core.Services
{
    /// <summary>
    /// Creates an <see cref="AuditThresholdConfiguration"/>
    /// This is a mock for now. This would eventually come from a configuration store of some sort
    /// </summary>
    public class AuditThresholdConfigurationFactory : IAuditThresholdConfigurationFactory
    {
        public AuditThresholdConfigurationFactory() { }

        /// <summary>
        /// Returns an <see cref="AuditThresholdConfiguration"/>
        /// </summary>
        /// <returns></returns>
        public AuditThresholdConfiguration Create()
        {
            return new AuditThresholdConfiguration()
            {
                LoginThreshold = TimeSpan.FromDays(1),
                PurchaseThreshold = 10d,
                SameIpThreshold = 10,
                SameMacThreshold = 3,
                SameSessionThreshold = 3
            };
        }
    }
}
