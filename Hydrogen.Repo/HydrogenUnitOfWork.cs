using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Hydrogen.Repo.Abstractions;

namespace Hydrogen.Repo;

public class HydrogenUnitOfWork(HydrogenContext hydrogenContext) : IHydrogenUnitOfWork
{
    public Task BeginTransactionAsync(CancellationToken ct)
    {
        return Task.Run(() =>
        {
            hydrogenContext.QueryFactory.Connection.Open();
            hydrogenContext.DbTransaction = hydrogenContext.QueryFactory.Connection.BeginTransaction();
        }, ct);
    }

    public Task<bool> IsOnTransactionAsync(CancellationToken ct)
    {
        return Task.Run(() =>
            hydrogenContext.DbTransaction != null 
            && hydrogenContext.QueryFactory.Connection.State == ConnectionState.Open, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct)
    {
        return Task.Run(() =>
        {
            if (hydrogenContext.DbTransaction == null)
            {
                throw new ArgumentNullException(nameof(hydrogenContext.DbTransaction), "The transaction cannot be null");
            }

            hydrogenContext.DbTransaction.Commit();
            hydrogenContext.QueryFactory.Connection.Close();
        }, ct);
    }

    public Task DiscardChangesAsync(CancellationToken ct)
    {
        return Task.Run(() =>
        {
            if (hydrogenContext.DbTransaction == null)
            {
                throw new ArgumentNullException(nameof(hydrogenContext.DbTransaction), "The transaction cannot be null");
            }

            hydrogenContext.DbTransaction.Rollback();
            hydrogenContext.QueryFactory.Connection.Close();
        }, ct);
    }
}