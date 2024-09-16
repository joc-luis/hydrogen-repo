using Hydrogen.Repo.Abstractions.DTO;
using Hydrogen.Repo.Tests.Interfaces.Repositories;
using Hydrogen.Repo.Tests.Models;

namespace Hydrogen.Repo.Tests.Repositories;

public class UserRepository(HydrogenContext hydrogenContext, string table) : HydrogenRepository<User, Guid>(hydrogenContext, table), IUserRepository
{
    
}