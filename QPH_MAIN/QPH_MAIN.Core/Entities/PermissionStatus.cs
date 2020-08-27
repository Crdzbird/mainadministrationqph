using System.Collections.Generic;
using System.Runtime.Serialization;

namespace QPH_MAIN.Core.Entities
{
    public partial class PermissionStatus
    {
        public int id { get; set; }
        public string permission { get; set; }
        public int statuses { get; set; }
        public bool status { get {
                return (statuses == 1) ? true : false;
            }
        }
    }
}