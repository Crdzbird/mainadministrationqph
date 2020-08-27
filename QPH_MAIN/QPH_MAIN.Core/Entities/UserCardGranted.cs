using System;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class UserCardGranted : BaseEntity
    {
        public UserCardGranted()
        {
            userCardPermission = new HashSet<UserCardPermission>();
        }

        public int id_user { get; set; }
        public int id_card { get; set; }
        public virtual User user { get; set; }
        public virtual Cards cards { get; set; }
        public virtual ICollection<UserCardPermission> userCardPermission { get; set; }
    }
}