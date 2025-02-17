using Nexus.Domain.Entities;
using Nexus.Infrastructure.Context;
using Nexus.Infrastructure.Repositories.Interfaces;

namespace Nexus.Infrastructure.Repositories.Implementations;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(NexusContext context) : base(context)
    {
    }
}
