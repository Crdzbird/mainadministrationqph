using QPH_MAIN.Core.QueryFilters;
using System;

namespace QPH_MAIN.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetActivationUri(string actionUrl);
        Uri GetPostPaginationUri(CityQueryFilter filter, string actionUrl);
        Uri GetPostPaginationUri(ViewQueryFilter filter, string actionUrl);
        Uri GetPostPaginationUri(BlacklistQueryFilter fliter, string actionUrl);
        Uri GetPostPaginationUri(CatalogQueryFilter filter, string actionUrl);
        Uri GetPostPaginationUri(RegionQueryFilter filter, string actionUrl);
        Uri GetPostPaginationUri(CountryQueryFilter filter, string actionUrl);
        Uri GetPostPaginationUri(PermissionsQueryFilter filter, string actionUrl);
        Uri GetPostPaginationUri(CardsQueryFilter filter, string actionUrl);
        Uri GetPostPaginationUri(RolesQueryFilter filter, string actionUrl);
        Uri GetPostPaginationUri(EnterpriseQueryFilter filter, string actionUrl);
        Uri GetPostPaginationUri(UserQueryFilter filter, string actionUrl);
    }
}