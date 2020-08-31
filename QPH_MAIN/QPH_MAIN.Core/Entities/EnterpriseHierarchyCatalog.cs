using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class EnterpriseHierarchyCatalog : BaseEntity
    {
        public EnterpriseHierarchyCatalog()
        {
            catalogs = new HashSet<Catalog>();
        }
        public int id_enterprise { get; set; }
        public int parent { get; set; }
        public int children { get; set; }
        public virtual Enterprise enterprise { get; set; }
        public virtual ICollection<Catalog> catalogs { get; set; }
    }
}