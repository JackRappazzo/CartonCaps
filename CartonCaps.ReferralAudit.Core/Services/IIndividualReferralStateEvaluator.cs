using CartonCaps.Persistence.Models;

namespace CartonCaps.ReferralAudit.Core.Services
{
    public interface IIndividualReferralStateEvaluator
    {
        Task<ReferralState> EvaluateReferralState(ReferredUser referral, CancellationToken cancellationToken);
    }
}