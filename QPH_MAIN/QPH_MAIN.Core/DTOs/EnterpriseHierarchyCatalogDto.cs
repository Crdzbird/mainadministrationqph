namespace QPH_MAIN.Core.DTOs
{
    public class EnterpriseHierarchyCatalogDto
    {
        public int Id { get; set; }
        public int EnterpriseId { get; set; }
        public int Parent { get; set; }
        public int Children { get; set; }
    }
}