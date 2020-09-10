using System.Collections.Generic;

namespace QPH_MAIN.Core.QueryFilters
{
    public class ViewQueryFilter
    {
        public string filter { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<SortModel> orderedBy { get; set; }
    }
}