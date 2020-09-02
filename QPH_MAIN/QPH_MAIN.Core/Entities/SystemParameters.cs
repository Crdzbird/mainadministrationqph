using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Core.Entities
{
    public class SystemParameters : BaseEntityCode
    {
        public string description { get; set; }
        public string value { get; set; }
        public string dataType { get; set; }
        public bool status { get; set; }
    }
}