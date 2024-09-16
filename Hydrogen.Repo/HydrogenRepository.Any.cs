using System.Threading;
using System.Threading.Tasks;
using Hydrogen.Repo.Abstractions;
using SqlKata;
using SqlKata.Execution;

namespace Hydrogen.Repo;

public abstract partial class HydrogenRepository<TModel, TId>
    where TModel : IHydrogenModel<TId>
{
    public virtual async Task<bool> AnyAsync(string column, string op, object value, CancellationToken ct = default)
    {
        var result = await hydrogenContext.QueryFactory.Query(table).Where(column, op, value).Limit(1).CountAsync<int>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);

        return result > 0;
    }
    public virtual async Task<bool> AnyAsync(string column, object value, CancellationToken ct = default)
    {
        return (await hydrogenContext.QueryFactory
            .Query(table)
            .Where(column,  value)
            .Limit(1)
            .CountAsync<int>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction)) > 0;
    }
    public virtual async Task<bool> AnyAsync(TId id, CancellationToken ct = default)
    {
        return (await hydrogenContext.QueryFactory
            .Query(table)
            .Where("Id", id)
            .Limit(1)
            .CountAsync<int>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction)) > 0;
    }
    public virtual async Task<bool> AnyAsync(CancellationToken ct = default)
    {
        return (await hydrogenContext.QueryFactory
            .Query(table).Limit(1)
            .CountAsync<int>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction)) > 0;
    }
    public virtual async Task<bool> AnyAsync(Query query, CancellationToken ct = default)
    {
        return (await hydrogenContext.QueryFactory
            .FromQuery(query)
            .From(table)
            .Limit(1)
            .CountAsync<int>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction)) > 0;
    }

}