using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class City : BaseEntity
    {
        public City()
        {
            enterprise = new HashSet<Enterprise>();
        }
        public int id_region { get; set; }
        public string name { get; set; }
        public virtual Region region { get; set; }
        public virtual ICollection<Enterprise> enterprise { get; set; }
    }
}