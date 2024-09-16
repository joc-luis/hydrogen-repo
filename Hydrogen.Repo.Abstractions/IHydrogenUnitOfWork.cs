using System.Threading;
using System.Threading.Tasks;

namespace Hydrogen.Repo.Abstractions
{
    public interface IHydrogenUnitOfWork
    {
        Task BeginTransactionAsync(CancellationToken ct);
        Task<bool> IsOnTransactionAsync(CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
        Task DiscardChangesAsync(CancellationToken ct);
    }
}