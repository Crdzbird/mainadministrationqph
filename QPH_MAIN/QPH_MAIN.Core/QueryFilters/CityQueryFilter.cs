namespace QPH_MAIN.Core.QueryFilters
{
    public class CityQueryFilter
    {
        public string filter { get; set; }
        public int? RegionId { get; set; }
        public string Name { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}