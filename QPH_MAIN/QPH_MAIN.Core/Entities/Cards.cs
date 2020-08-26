using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class CardsPermissionStatus : BaseEntity
    {
        public string card { get; set; }
        public List<PermissionStatus> permissionStatuses { get; set; }
    }
}