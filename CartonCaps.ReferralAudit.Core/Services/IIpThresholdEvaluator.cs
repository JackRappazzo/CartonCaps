using CartonCaps.Persistence.Models;

namespace CartonCaps.ReferralAudit.Core.Services
{
    public interface IIpThresholdEvaluator
    {
        IEnumerable<Guid> GetReferralIdsThatExceedIpThreshold(string ipAddress, IEnumerable<ReferredUser> referrals, int threshold);
    }
}