using QPH_MAIN.Core.Entities;
using System;

namespace QPH_MAIN.Core.Entities
{
    public class TableColumn : BaseEntityCode
    {
        public string Typename { get; set; }
        public Int16 MaxLength { get; set; }
    }
}