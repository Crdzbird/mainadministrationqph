using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Region : BaseEntity
    {
        public Region()
        {
            city = new HashSet<City>();
        }

        public int id_country { get; set; }
        public string name { get; set; }
        public virtual Country country { get; set; }
        public virtual ICollection<City> city { get; set; }
    }
}