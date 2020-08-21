using System.Collections.Generic;

namespace QPH_MAIN.Core.DTOs
{
    public class TreeDto
    {
        public TreeDto()
        {
            Children = new List<TreeDto>();
        }

        public int son { get; set; }
        public int parent { get; set; }
        public string title { get; set; }
        public List<TreeDto> Children { get; set; }
    }
}