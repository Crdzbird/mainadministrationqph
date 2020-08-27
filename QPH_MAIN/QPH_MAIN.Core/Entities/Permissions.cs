using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Permissions : BaseEntity
    {
        public Permissions()
        {
            userCardPermissions = new HashSet<UserCardPermission>();
        }

        public string permission { get; set; }
        public virtual ICollection<UserCardPermission> userCardPermissions { get; set; }
    }
}