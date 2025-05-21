using CartonCaps.Persistence.Models;

namespace CartonCaps.ReferralAudit.Core.Services
{
    public interface ISessionIdThresholdEvaluator
    {
        IEnumerable<Guid> GetReferralIdsThatExceedSessionIdThreshold(Guid sessionId, IEnumerable<ReferredUser> referrals, int threshold);
    }
}