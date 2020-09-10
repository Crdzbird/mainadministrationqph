using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class User : BaseEntity
    {

        public User()
        {
            userViews = new HashSet<UserView>();
            userCardGranted = new HashSet<UserCardGranted>();
        }

        public int id_role { get; set; }
        public int id_enterprise { get; set; }
        public int id_country { get; set; }
        public string nickname { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string hashPassword { get; set; }
        public string google_access_token { get; set; }
        public string facebook_access_token { get; set; }
        public string firebase_token { get; set; }
        public bool is_account_activated { get; set; }
        public string profile_picture { get; set; }
        public bool status { get; set; }
        public string activation_code { get; set; }
        public virtual Enterprise enterprise { get; set; }
        public virtual Country country { get; set; }
        public virtual Roles roles { get; set; }
        public virtual ICollection<UserCardGranted> userCardGranted { get; set; }
        public virtual ICollection<UserView> userViews { get; set; }
    }
}