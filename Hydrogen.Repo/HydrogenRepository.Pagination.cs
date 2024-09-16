using System.Threading;
using System.Threading.Tasks;
using Hydrogen.Repo.Abstractions;
using Hydrogen.Repo.Abstractions.DTO;
using Hydrogen.Repo.Extensions;
using SqlKata;

namespace Hydrogen.Repo;

public abstract partial class HydrogenRepository<TModel, TId>
    where TModel : IHydrogenModel<TId>
{
      public Task<PaginationData<TEntity>> GetPaginationAsync<TEntity>(int page, int take,
            CancellationToken ct = default)
        {
            return hydrogenContext.QueryFactory.Query(table)
                .GetPaginationResultAsync<TEntity>(hydrogenContext.DbTransaction, page, take, ct);
        }
        
        public Task<PaginationData<TEntity>> GetPaginationAsync<TEntity>(string column, object value, int page, int take, CancellationToken ct = default)
        {
            return GetPaginationAsync<TEntity>(column, "=", value, page, take, ct);
        }
        
        public Task<PaginationData<TEntity>> GetPaginationAsync<TEntity>(string column, string op, object value, int page, int take, CancellationToken ct = default)
        {
            return hydrogenContext.QueryFactory
                .Query(table)
                .Where(column, op, value)
                .GetPaginationResultAsync<TEntity>(hydrogenContext.DbTransaction, page, take, ct);
        }
        
        public Task<PaginationData<TEntity>> GetPaginationAsync<TEntity>(Query query, int page, int take,
            CancellationToken ct = default)
        {
            return hydrogenContext.QueryFactory
                .FromQuery(query)
                .From(table)
                .GetPaginationResultAsync<TEntity>(hydrogenContext.DbTransaction, page, take, ct);
        }
        public Task<PaginationData<TModel>> GetPaginationAsync(int page, int take, CancellationToken ct = default)
        {
            return GetPaginationAsync<TModel>(page, take, ct);
        }
        public Task<PaginationData<TModel>>
            GetPaginationAsync(Query query, int page, int take, CancellationToken ct = default)
        {
            return GetPaginationAsync<TModel>(query, page, take, ct);
        }
        public Task<PaginationData<TModel>> GetPaginationAsync(string column, object value, int page, int take, CancellationToken ct = default)
        {
            return GetPaginationAsync<TModel>(column, "=", value, page, take, ct);
        }
        public Task<PaginationData<TModel>> GetPaginationAsync(string column, string op, object value, int page, int take, CancellationToken ct = default)
        {
            return GetPaginationAsync<TModel>(column, op, value, page, take, ct);
        }
        
}