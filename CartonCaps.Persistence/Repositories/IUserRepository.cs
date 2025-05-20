using CartonCaps.Persistence.Models;

namespace CartonCaps.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<CartonCapsUser?> FetchUserById(Guid userId, CancellationToken cancellationToken);
    }
}