using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Cards : BaseEntity
    {
        public Cards()
        {
            userCardGranted = new HashSet<UserCardGranted>();
            viewCard = new HashSet<ViewCard>();
        }

        public string card { get; set; }
        public virtual ICollection<UserCardGranted> userCardGranted { get; set; }
        public virtual ICollection<ViewCard> viewCard { get; set; }
    }
}