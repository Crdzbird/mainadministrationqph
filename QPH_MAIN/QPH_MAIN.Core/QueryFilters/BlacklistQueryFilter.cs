using System.Collections.Generic;

namespace QPH_MAIN.Core.QueryFilters
{
    public class BlacklistQueryFilter
    {
        public string filter { get; set; }
        public string Ip { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<SortModel> orderedBy { get; set; }
    }
}