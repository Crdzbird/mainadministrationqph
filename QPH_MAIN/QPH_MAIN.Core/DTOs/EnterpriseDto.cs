using System;

namespace QPH_MAIN.Core.DTOs
{
    public class EnterpriseDto
    {
        public int Id { get; set; }
        public int id_city { get; set; }
        public string commercial_name { get; set; }
        public string name_application { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string enterprise_address { get; set; }
        public string identification { get; set; }
        public bool has_branches { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public DateTime created_at { get; set; }
        public bool status { get; set; }
    }
}