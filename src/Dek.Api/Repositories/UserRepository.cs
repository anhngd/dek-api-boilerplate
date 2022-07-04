using Dek.Api.Contexts;
using Dek.Api.Entities;

namespace Dek.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetAsync(Guid guid, CancellationToken cancellationToken)
    {
        return Task.FromResult(this._context.Users.FirstOrDefault(i => i.Id == guid));
    }

    public async Task<User> AddAsync(User item, CancellationToken cancellationToken)
    {
        _ = await _context.AddAsync(item, cancellationToken);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<User> UpdateAsync(User item, CancellationToken cancellationToken)
    {
        _ = _context.Update(item);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return item;
    }

    public Task<User> DeleteAsync(User item, CancellationToken cancellationToken)
    {
        _ = _context.Remove(item);
        _ = _context.SaveChangesAsync(cancellationToken);
        return Task.FromResult(item);
    }
}