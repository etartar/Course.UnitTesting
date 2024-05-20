using Microsoft.EntityFrameworkCore;
using Users.Api.Contexts;
using Users.Api.Models;

namespace Users.Api.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.ToListAsync(cancellationToken);
    }

    public async Task<bool> NameIsExist(string fullName, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(p => p.FullName == fullName, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(user, cancellationToken);
        
        int result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Remove(user);

        int result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}
