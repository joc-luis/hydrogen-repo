using System.Threading;
using System.Threading.Tasks;
using Hydrogen.Repo.Abstractions;
using SqlKata;
using SqlKata.Execution;

namespace Hydrogen.Repo;

public abstract partial class HydrogenRepository<TModel, TId>
    where TModel : IHydrogenModel<TId>
{
    public virtual async Task<long> CountAsync(string column, object value, CancellationToken ct = default)
    {
        return (await hydrogenContext.QueryFactory
            .Query(table)
            .Where(column, value)
            .CountAsync<long>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction));
    }
    public virtual async Task<long> CountAsync(CancellationToken ct = default)
    {
        return (await hydrogenContext.QueryFactory
            .Query(table)
            .CountAsync<long>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction));
    }
    public virtual async Task<long> CountAsync(Query query, CancellationToken ct = default)
    {
        return (await hydrogenContext.QueryFactory
            .FromQuery(query)
            .From(table)
            .CountAsync<long>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction));
    }
    public virtual Task<long> CountAsync(string column, string op, object value, CancellationToken ct = default)
    {
        return hydrogenContext.QueryFactory.Query(table).Where(column, op, value).Limit(1).CountAsync<long>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
    }
}