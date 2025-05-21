using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;

namespace CartonCaps.ReferralAudit.Core.Services
{
    public class IpThresholdEvaluator : IIpThresholdEvaluator
    {
        public IpThresholdEvaluator()
        {

        }

        public IEnumerable<Guid> GetReferralIdsThatExceedIpThreshold(string ipAddress, IEnumerable<ReferredUser> referrals, int threshold)
        {
            var referralsSharingIpAddress = referrals.Where(r => r.ReferredIpAddress == ipAddress);
            if (referralsSharingIpAddress.Count() > threshold)
            {
                return referralsSharingIpAddress.Select(r => r.Id);
            }
            else return [];
        }
    }
}
