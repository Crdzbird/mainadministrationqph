using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Cards : BaseEntity
    {
        public Cards()
        {
            userCardGranted = new HashSet<UserCardGranted>();
        }

        public string card { get; set; }
        public virtual ICollection<UserCardGranted> userCardGranted { get; set; }
    }
}