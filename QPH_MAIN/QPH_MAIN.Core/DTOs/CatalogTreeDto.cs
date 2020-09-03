using QPH_MAIN.Core.Entities;
using System.Collections.Generic;

namespace QPH_MAIN.Core.DTOs
{
    public class CatalogTreeDto
    {
        public CatalogTreeDto()
        {
            Children = new List<CatalogTreeDto>();
        }

        public int son { get; set; }
        public int parent { get; set; }
        public string title { get; set; }
        public string code { get; set; }
        public List<CatalogTreeDto> Children { get; set; }
    }
}