using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hydrogen.Repo.Abstractions;
using SqlKata;
using SqlKata.Execution;

namespace Hydrogen.Repo;

public abstract partial class HydrogenRepository<TModel, TId>
    where TModel : IHydrogenModel<TId>
{
    public Task<IEnumerable<TEntity>> GetAsync<TEntity>(CancellationToken ct = default)
    {
        return hydrogenContext.QueryFactory.Query(table)
            .GetAsync<TEntity>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
    }
    public Task<IEnumerable<TEntity>> GetAsync<TEntity>(string column, object value, CancellationToken ct = default)
    {
        return GetAsync<TEntity>(column, "=", value, ct);
    }
    public Task<IEnumerable<TEntity>> GetAsync<TEntity>(string column, string op, object value, CancellationToken ct = default)
    {
        return hydrogenContext.QueryFactory.Query(table)
            .Where(column, op, value)
            .GetAsync<TEntity>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
    }
    public Task<IEnumerable<TEntity>> GetAsync<TEntity>(Query query, CancellationToken ct = default)
    {
        return hydrogenContext.QueryFactory.FromQuery(query).From(table)
            .GetAsync<TEntity>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
    }
    public Task<IEnumerable<TModel>> GetAsync(CancellationToken ct = default)
    {
        return GetAsync<TModel>(ct);
    }
    public Task<IEnumerable<TModel>> GetAsync(string column, object value, CancellationToken ct = default)
    {
        return GetAsync<TModel>(column, value, ct);
    }
    public Task<IEnumerable<TModel>> GetAsync(string column, string op, object value, CancellationToken ct = default)
    {
        return GetAsync<TModel>(column, op, value, ct);
    }
        
    public Task<IEnumerable<TModel>> GetAsync(Query query, CancellationToken ct = default)
    {
        return GetAsync<TModel>(query, ct);
    }
}