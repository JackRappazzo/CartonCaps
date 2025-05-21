using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CartonCaps.Persistence.Models;

namespace CartonCaps.ReferralAudit.Core.Services
{
    public class SessionIdThresholdEvaluator : ISessionIdThresholdEvaluator
    {
        public IEnumerable<Guid> GetReferralIdsThatExceedSessionIdThreshold(Guid sessionId, IEnumerable<ReferredUser> referrals, int threshold)
        {
            var referralsSharingSessionId = referrals.Where(r => r.ReferredSessionId == sessionId);
            if (referralsSharingSessionId.Count() > threshold)
            {
                return referralsSharingSessionId.Select(r => r.Id);
            }
            else return [];
        }
    }
}
