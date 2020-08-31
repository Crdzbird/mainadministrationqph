using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class EnterpriseHierarchyCatalog : BaseEntity
    {
        public int id_enterprise { get; set; }
        public int parent { get; set; }
        public int children { get; set; }
        public virtual Enterprise enterprise { get; set; }
        public virtual Catalog catalog { get; set; }
    }
}