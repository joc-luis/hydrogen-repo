using System;
using System.Threading;
using System.Threading.Tasks;
using Hydrogen.Repo.Abstractions;
using SqlKata;
using SqlKata.Execution;

namespace Hydrogen.Repo;

public abstract partial class HydrogenRepository<TModel, TId>
    where TModel : IHydrogenModel<TId>
{
    public async Task<TEntity> FirstAsync<TEntity>(TId id, CancellationToken ct = default)
        {
            var entity = await hydrogenContext.QueryFactory
                .Query(table)
                .Where("Id", id)
                .FirstOrDefaultAsync<TEntity>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);

            if (entity == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return entity;
        }
        public async Task<TEntity> FirstAsync<TEntity>(string column, object value, CancellationToken ct = default)
        {
            var entity = await hydrogenContext.QueryFactory
                .Query(table)
                .Where(column, value)
                .FirstOrDefaultAsync<TEntity>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);

            if (entity == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return entity;
        }
        
        public Task<TEntity> FirstAsync<TEntity>(string column, string op, object value, CancellationToken ct = default)
        {
            return hydrogenContext.QueryFactory.Query(table)
                .Where(column, op, value)
                .FirstAsync<TEntity>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }
        public async Task<TEntity> FirstAsync<TEntity>(Query query, CancellationToken ct = default)
        {
            var entity = await hydrogenContext.QueryFactory
                .FromQuery(query)
                .From(table)
                .FirstOrDefaultAsync<TEntity>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);

            if (entity == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return entity;
        }
        public Task<TEntity?> FirstOrDefaultAsync<TEntity>(TId id, CancellationToken ct = default)
        {
            return hydrogenContext.QueryFactory
                .Query(table)
                .Where("Id", id)
                .FirstOrDefaultAsync<TEntity?>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }
        public Task<TEntity?> FirstOrDefaultAsync<TEntity>(string column, object value, CancellationToken ct = default)
        {
            return hydrogenContext.QueryFactory
                .Query(table)
                .Where(column, value)
                .FirstOrDefaultAsync<TEntity?>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }
        public Task<TEntity?> FirstOrDefaultAsync<TEntity>(string column, string op, object value, CancellationToken ct = default)
        {
            return hydrogenContext.QueryFactory.Query(table)
                .Where(column, op, value)
                .FirstOrDefaultAsync<TEntity?>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }
        public Task<TEntity?> FirstOrDefaultAsync<TEntity>(Query query, CancellationToken ct = default)
        {
            return hydrogenContext.QueryFactory
                .FromQuery(query)
                .From(table)
                .FirstOrDefaultAsync<TEntity?>(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }
        public Task<TModel> FirstAsync(TId id, CancellationToken ct = default)
        {
            return FirstAsync<TModel>(id, ct);
        }
        public Task<TModel> FirstAsync(string column, object value, CancellationToken ct = default)
        {
            return FirstAsync<TModel>(column, value, ct);
        }
        public Task<TModel> FirstAsync(string column, string op, object value, CancellationToken ct = default)
        {
            return FirstAsync<TModel>(column, op, value, ct);
        }
        public Task<TModel> FirstAsync(Query query, CancellationToken ct = default)
        {
            return FirstAsync<TModel>(query, ct);
        }
        public Task<TModel?> FirstOrDefaultAsync(TId id, CancellationToken ct = default)
        {
            return FirstOrDefaultAsync<TModel>(id, ct);
        }
        public Task<TModel?> FirstOrDefaultAsync(string column, object value, CancellationToken ct = default)
        {
            return FirstOrDefaultAsync<TModel>(column, value, ct);
        }
        public Task<TModel?> FirstOrDefaultAsync(Query query, CancellationToken ct = default)
        {
            return FirstOrDefaultAsync<TModel>(query, ct);
        }
        public Task<TModel?> FirstOrDefaultAsync(string column, string op, object value, CancellationToken ct = default)
        {
            return FirstOrDefaultAsync<TModel?>(column, op, value, ct);
        }
}