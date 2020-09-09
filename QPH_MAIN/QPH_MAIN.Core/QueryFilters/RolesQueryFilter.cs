using System.Collections.Generic;

namespace QPH_MAIN.Core.QueryFilters
{
    public class RolesQueryFilter
    {
        public string filter { get; set; }
        public string rolename { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<SortModel> orderedBy { get; set; }
    }
}