using CartonCaps.Persistence.Models;

namespace CartonCaps.Persistence.Repositories
{
    public interface IReferralRepository
    {
        Task<IEnumerable<ReferredUser>> GetReferredUsersByReferringId(Guid referringUserId, CancellationToken cancellationToken);
    }
}