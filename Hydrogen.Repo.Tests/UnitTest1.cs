using Hydrogen.Repo.Enums;
using Hydrogen.Repo.Tests.Models;
using Hydrogen.Repo.Tests.Repositories;
using Npgsql;
using SqlKata;

namespace Hydrogen.Repo.Tests;

public class Tests
{
    private HydrogenContext context;
    [SetUp]
    public void Setup()
    {
        var connectionBuilder = new NpgsqlConnectionStringBuilder()
        {
            Host = "localhost",
            Port = 5432,
            Username = "postgres",
            Password = "1234567890",
            Database = "HydrogenAdvanced"
        };
        
        var npgsqlConnection = new NpgsqlConnection(connectionBuilder.ConnectionString);

        context = new HydrogenContext(npgsqlConnection, DbConnectionType.Npgsql);
    }

    [Test]
    public async Task Any()
    {
        var userRepository = new UserRepository(context, "Users");
        const string email = "admin@email.com";
        var results = new bool[]
        {
            await userRepository.AnyAsync(),
            await userRepository.AnyAsync("Email", email),
            await userRepository.AnyAsync("Email", "=", email),
            await userRepository.AnyAsync(new Query().Where("Email", email))
        };

        await userRepository.GetAsync("Email", "=", "", default);
        
        Assert.IsTrue(results.All(r => r));
    }

    [Test]
    public async Task BulkInsertTestAsync()
    {
        var userRepository = new UserRepository(context, "Users");
        var users = new[]
        {
            new User()
            {
                Email = Guid.NewGuid().ToString(),
                Password = "1234567890",
                RoleId = Guid.NewGuid(),
                TwoFactor = false
            },
            new User()
            {
                Email = Guid.NewGuid().ToString(),
                Password = "1234567890",
                RoleId = Guid.NewGuid(),
                TwoFactor = false
            }
        };
        
        await userRepository.InsertAsync(users);
    }

    [Test]
    public async Task InsertTestAsync()
    {
        var userRepository = new UserRepository(context, "Users");
        await userRepository.InsertAsync(new User()
        {
            Email = Guid.NewGuid().ToString(),
            Password = "1234567890",
            RoleId = Guid.NewGuid(),
            TwoFactor = false
        });
    }
    
    [Test]
    public async Task UpdateTestAsync()
    {
        var userRepository = new UserRepository(context, "Users");
        var users = await userRepository.GetAsync();

        foreach (var user in users)
        {
            user.Email = Guid.NewGuid().ToString();
            await userRepository.UpdateAsync(user);
        }
    }
}