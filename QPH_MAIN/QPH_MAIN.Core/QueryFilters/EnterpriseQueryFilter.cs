using System.Collections.Generic;

namespace QPH_MAIN.Core.QueryFilters
{
    public class EnterpriseQueryFilter
    {
        public string filter { get; set; }
        public int? id_city { get; set; }
        public string commercial_name { get; set; }
        public string name_application { get; set; }
        public string enterprise_address { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<SortModel> orderedBy { get; set; }
    }
}