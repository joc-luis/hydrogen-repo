using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Hydrogen.Repo.Abstractions.DTO;
using SqlKata;
using SqlKata.Execution;

namespace Hydrogen.Repo.Extensions;

internal static  class SqlKataExtensions
{
    internal static async Task<PaginationData<TEntity>> GetPaginationResultAsync<TEntity>(this Query query,
        IDbTransaction transaction,
        int page = 1, int take = 25, CancellationToken ct = default)
    {
        PaginationData<TEntity> pagination = new();

        var result = await query.PaginateAsync<TEntity>(page, take, cancellationToken: ct, transaction: transaction);

        pagination.Page = result.Page;
        pagination.Take = result.PerPage;
        pagination.Items = result.List;
        pagination.Pages = result.TotalPages;
        pagination.Total = result.Count;

        return pagination;
    }
}