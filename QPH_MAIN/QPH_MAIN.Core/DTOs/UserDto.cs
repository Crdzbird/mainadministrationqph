namespace QPH_MAIN.Core.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public int id_role { get; set; }
        public int id_enterprise { get; set; }
        public int id_country { get; set; }
        public string nickname { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string hashPassword { get; set; }
        public string activation_code { get; set; }
        public string google_access_token { get; set; }
        public string facebook_access_token { get; set; }
        public string firebase_token { get; set; }
        public bool is_account_activated { get; set; }
        public string profile_picture { get; set; }
        public bool status { get; set; }
    }
}