using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Catalog : BaseEntity
    {
        public Catalog()
        {
            enterpriseCatalog = new HashSet<EnterpriseHierarchyCatalog>();
        }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public virtual ICollection<EnterpriseHierarchyCatalog> enterpriseCatalog { get; set; }
    }
}