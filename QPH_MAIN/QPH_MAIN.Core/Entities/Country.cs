using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Country : BaseEntity
    {
        public Country()
        {
            users = new HashSet<User>();
            regions = new HashSet<Region>();
        }

        public string name { get; set; }
        public virtual ICollection<User> users { get; set; }
        public virtual ICollection<Region> regions { get; set; }
    }
}