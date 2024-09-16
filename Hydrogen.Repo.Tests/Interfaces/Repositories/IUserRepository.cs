using Hydrogen.Repo.Abstractions;
using Hydrogen.Repo.Tests.Models;

namespace Hydrogen.Repo.Tests.Interfaces.Repositories;

public interface IUserRepository : IHydrogenRepository<User, Guid>
{
    
}