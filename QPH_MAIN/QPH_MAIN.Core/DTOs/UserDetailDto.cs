using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Core.DTOs
{
    public partial class UserDetailDto
    {
        public int Id_role { get; set; }
        public int Id_enterprise { get; set; }
        public int Id_country { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Phone_number { get; set; }
        public string Profile_picture { get; set; }
        public virtual Enterprise Enterprise { get; set; }
        public virtual Country Country { get; set; }
        public virtual Tree Tree { get; set; }
    }
}