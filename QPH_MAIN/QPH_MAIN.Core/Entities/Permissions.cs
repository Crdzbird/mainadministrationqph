using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Permissions : BaseEntity
    {
        /*public Permissions()
        {
            userCardPermissions = new HashSet<UserCardPermissions>();
        }*/

        public string permission { get; set; }
        //public virtual ICollection<UserCardPermissions> userCardPermissions { get; set; }
    }
}