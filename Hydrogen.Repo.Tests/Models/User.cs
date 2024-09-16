using Hydrogen.Repo.Abstractions;
using Hydrogen.Repo.Abstractions.Attributes;

namespace Hydrogen.Repo.Tests.Models;

public class User : IHydrogenModel<Guid>
{
    [AutoGenerateGuid()]
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public string Email {get; set; }
    public string Password {get; set; }
    public bool TwoFactor { get; set; }
}