using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.ReferralAudit.Core.Services
{
    public class AuditThresholdConfiguration
    {
        public int SameSessionThreshold { get; set; }
        public int SameMacThreshold { get; set; }
        public int SameIpThreshold { get; set; }

        public TimeSpan LoginThreshold { get; set; }
        public double PurchaseThreadhold { get; set; }
    }
}
