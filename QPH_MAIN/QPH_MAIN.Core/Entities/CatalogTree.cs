using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class CatalogTree : BaseEntity
    {
        public CatalogTree()
        {
            Children = new List<CatalogTree>();
        }
        public string title { get; set; }
        public int son { get; set; }
        public int parent { get; set; }
        public string code { get; set; }
        public List<CatalogTree> Children { get; set; }
    }
}