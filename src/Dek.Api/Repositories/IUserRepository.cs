using Dek.Api.Entities;

namespace Dek.Api.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid guid, CancellationToken cancellationToken);
    Task<User> AddAsync(User item, CancellationToken cancellationToken);
    Task<User> UpdateAsync(User item, CancellationToken cancellationToken);
    Task<User> DeleteAsync(User item, CancellationToken cancellationToken);
}
