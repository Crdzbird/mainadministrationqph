using System.Collections.Generic;

namespace QPH_MAIN.Core.QueryFilters
{
    public class UserQueryFilter
    {
        public string filter { get; set; }
        public int? RoleId { get; set; }
        public int? EnterpriseId { get; set; }
        public int? CountryId { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Status { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<SortModel> orderedBy { get; set; }
    }
}