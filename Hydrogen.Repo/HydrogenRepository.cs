using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Hydrogen.Repo.Abstractions;
using Hydrogen.Repo.Abstractions.Attributes;
using Newtonsoft.Json;
using SqlKata;
using SqlKata.Execution;

namespace Hydrogen.Repo
{
    public abstract partial class HydrogenRepository<TModel, TId>(HydrogenContext hydrogenContext, string table)
        where TModel : IHydrogenModel<TId>
    {
        private const string Identifier = "Id";
        
        public virtual async Task InsertAsync(TModel store, CancellationToken ct = default)
        {

            var data = GetValues(store, true);
            
            await hydrogenContext.QueryFactory
                .Query(table)
                .InsertAsync(data, cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }

        public virtual async Task<TId> InsertGetIdAsync(TModel store, CancellationToken ct = default)
        {
            var data = GetValues(store, true);

            bool auto = store.GetType()
                .GetProperties()
                .First(p => p.Name == "Id")
                .GetCustomAttributes()
                .Any(a => a.GetType() == typeof(AutoGenerateGuidAttribute));

            if (auto)
            {
                await hydrogenContext.QueryFactory
                    .Query(table)
                    .InsertAsync(data, transaction: hydrogenContext.DbTransaction, cancellationToken: ct);

                return (TId)data["Id"];
            }

            return await hydrogenContext.QueryFactory
                .Query(table)
                .InsertGetIdAsync<TId>(data, transaction: hydrogenContext.DbTransaction, cancellationToken: ct);
        }

        public virtual async Task InsertAsync(IEnumerable<TModel> items, CancellationToken ct = default)
        {
            var properties = items.First().GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttributes()
                    .All(p => p.GetType() != typeof(AutoGenerateFieldAttribute)));

            IEnumerable<string> fields = properties.Select(p => p.Name);
            var values = new List<IEnumerable<Object>>();

            foreach (TModel item in items)
            {
                values.Add(properties.Select(p =>
                    p.GetCustomAttributes().Any(a => a.GetType() == typeof(JsonFieldAttribute))
                        ? JsonConvert.SerializeObject(p.GetValue(item))
                        : p.GetCustomAttributes().Any(a => a.GetType() == typeof(AutoGenerateGuidAttribute))
                            ? Guid.NewGuid()
                            : p.GetValue(item)));
            }


            await hydrogenContext.QueryFactory.Query(table)
                .InsertAsync(fields, values, cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }

        public virtual async Task UpdateAsync(TModel update, CancellationToken ct = default)
        {
            var data = GetValues(update);
            
            await hydrogenContext.QueryFactory.Query(table)
                .Where("Id", update.Id)
                .UpdateAsync(data, cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }
        
        public virtual async Task DestroyAsync(Guid id, CancellationToken ct = default)
        {
            await hydrogenContext.QueryFactory.Query(table)
                .Where("Id", id)
                .DeleteAsync(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }

        public virtual Task DestroyAsync(string column, object value, CancellationToken ct = default)
        {
            return DestroyAsync(column, "=", value, ct);
        }

        public virtual Task DestroyAsync(string column, string op, object value, CancellationToken ct = default)
        {
            return hydrogenContext.QueryFactory.Query(table)
                .Where(column, op, value)
                .DeleteAsync(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }

        public virtual async Task DestroyAsync(Query query, CancellationToken ct = default)
        {
            await hydrogenContext.QueryFactory
                .FromQuery(query)
                .From(table)
                .DeleteAsync(cancellationToken: ct, transaction: hydrogenContext.DbTransaction);
        }

        private IDictionary<string, object> GetValues<TEntity>(TEntity entity, bool includeId = false)
        {
            var properties = entity.GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttributes()
                    .All(p => p.GetType() != typeof(AutoGenerateFieldAttribute)))
                .Where(p => (p.Name != Identifier || includeId));

            var data = new ExpandoObject() as IDictionary<string, object>;

            foreach (var property in properties)
            {
                if (property.GetCustomAttributes().Any(a => a.GetType() == typeof(JsonFieldAttribute)))
                {
                    data[property.Name] = JsonConvert.SerializeObject(property.GetValue(entity));
                }
                else if (property.GetCustomAttributes().Any(a => a.GetType() == typeof(AutoGenerateGuidAttribute)))
                {
                    data[property.Name] = Guid.NewGuid();
                }
                else
                {
                    data[property.Name] = property.GetValue(entity);
                }
            }

            return data;
        }
    }
}