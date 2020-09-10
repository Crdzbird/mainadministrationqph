using System;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Enterprise : BaseEntity
    {
        public Enterprise()
        {
            users = new HashSet<User>();
            enterpriseCatalog = new HashSet<EnterpriseHierarchyCatalog>();
        }

        public int id_city { get; set; }
        public string commercial_name { get; set; }
        public string name_application { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string enterprise_address { get; set; }
        public string identification { get; set; }
        public bool has_branches { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public DateTime created_at { get; set; }
        public bool status { get; set; }
        public virtual City city { get; set; }
        public virtual ICollection<User> users { get; set; }
        public virtual ICollection<EnterpriseHierarchyCatalog> enterpriseCatalog { get; set; }
    }
}