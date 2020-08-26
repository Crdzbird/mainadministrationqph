using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class ViewCard : BaseEntity
    {
        public int id_view { get; set; }
        public int id_card { get; set; }
        public virtual Views view { get; set; }
        public virtual Cards card { get; set; }
    }
}