using CartonCaps.Persistence.Models;

namespace CartonCaps.Persistence.Repositories
{
    /// <summary>
    /// Manages users in persistent storage
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves the given user. Returns null if not found
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CartonCapsUser?> FetchUserById(Guid userId, CancellationToken cancellationToken);
        Task<string?> FetchUsersReferralCode(Guid userId, CancellationToken cancellationToken);
    }
}