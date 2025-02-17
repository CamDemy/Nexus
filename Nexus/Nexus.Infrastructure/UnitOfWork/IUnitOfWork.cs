using Nexus.Infrastructure.Repositories.Interfaces;

namespace Nexus.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync();
}
