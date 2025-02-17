using Nexus.Infrastructure.Context;
using Nexus.Infrastructure.Repositories.Implementations;
using Nexus.Infrastructure.Repositories.Interfaces;

namespace Nexus.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly NexusContext _context;

    public UnitOfWork(NexusContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
    }

    public IUserRepository Users { get; }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
