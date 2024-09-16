# Hydrogen Repo

This package uses [SqlKata](https://sqlkata.com/) as query builder, if you have doubts check its [documentation](https://sqlkata.com/docs).

## Installation

````shell
dotnet add package Hydrogen.Repo
dotnet add package Hydrogen.Repo.Abstractions
````

## Settings

````csharp
var connectionBuilder = new NpgsqlConnectionStringBuilder()
{
    Host = "localhost",
    Port = 5432,
    Username = "postgres",
    Password = "1234567890",
    Database = "master"
};
        
var npgsqlConnection = new NpgsqlConnection(connectionBuilder.ConnectionString);

collection.AddScoped(_ => new HydrogenContext(connection, DbConnectionType.Npgsql));
collection.AddScoped<IHydrogenUnitOfWork, HydrogenUnitOfWork>(); // Only if you need transactions
````

## Repositories

### Create a model
The model interface, implements the Id field and a dynamic type.
````csharp
public class User : IHydrogenModel<Guid>
{
      [AutoGenerateGuid]
      public Guid Id {  get; set; }
      public Guid RoleId { get; set; }
      public string Name { get; set; }
      public string LastName { get; set; }
      public string? Email { get; set; }
      public string? PhoneNumber { get; set; }
      public string Password { get; set; }
}
````

### Create repository
The repository already implements several basic functions.

````csharp
public interface IUserRepository : IHydrogenRepository<User, Guid>
{
    // Add your functions
}
````
The abstract class implements the functions of the base interface.

````csharp
public class UserRepository : HydrogenRepository<User, Guid>, IUserRepository
{
    public UserRepository(HydrogenContext hydrogenContext) : base(hydrogenContext, "Users" //<- Table name)
    {
    }
}
````
### Add to the service collection
````csharp
var connectionBuilder = new NpgsqlConnectionStringBuilder()
{
    Host = "localhost",
    Port = 5432,
    Username = "postgres",
    Password = "1234567890",
    Database = "master"
};
        
var npgsqlConnection = new NpgsqlConnection(connectionBuilder.ConnectionString);

collection.AddScoped(_ => new HydrogenContext(connection, DbConnectionType.Npgsql));
collection.AddScoped<IHydrogenUnitOfWork, HydrogenUnitOfWork>();
collection.AddScoped<IUserRepository, UserRepository>();
````

### Use in a service
````csharp
private readonly IUserRepository _userRepository;

public OptionService(IUserRepository userRepository)
{
    _userRepository = userRepository;
}

public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken ct)
{
    return _userRepository.GetAsync<UserDto>(ct);
}
````

## Available functions

* Any
* Count
* Insert
* Bulk insert
* Update
* Delete
* Get
* Pagination
* First
* First or default