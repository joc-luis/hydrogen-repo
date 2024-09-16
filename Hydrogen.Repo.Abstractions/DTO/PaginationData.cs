using System.Collections.Generic;

namespace Hydrogen.Repo.Abstractions.DTO
{
    public class PaginationData<TRow>
    {
        public int Page { get; set; }
        public int Take { get; set; }
        public long Total { get; set; }
        public int Pages { get; set; }
        public IEnumerable<TRow> Items { get; set; } = new List<TRow>();
    }
}