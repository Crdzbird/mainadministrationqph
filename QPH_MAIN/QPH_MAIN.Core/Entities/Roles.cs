using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Roles : BaseEntity
    {
        public Roles()
        {
            users = new HashSet<User>();
        }

        public string rolename { get; set; }
        public bool status { get; set; }
        public virtual ICollection<User> users { get; set; }
    }
}