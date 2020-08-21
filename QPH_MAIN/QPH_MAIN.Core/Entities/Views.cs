using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Views : BaseEntity
    {
        public Views()
        {
            userView = new HashSet<UserView>();
        }
        public string code { get; set; }
        public string name { get; set; }
        public virtual ICollection<UserView> userView { get; set; }
    }
}