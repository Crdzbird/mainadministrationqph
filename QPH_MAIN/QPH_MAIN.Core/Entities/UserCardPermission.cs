using System;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class UserCardPermission : BaseEntity
    {
        public int id_card_granted { get; set; }
        public int id_permission { get; set; }
        public virtual UserCardGranted userCardGranted { get; set; }
        public virtual Permissions permissions { get; set; }
    }
}