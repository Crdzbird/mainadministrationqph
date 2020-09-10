using System;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository CityRepository { get; }
        IBlacklistRepository BlacklistRepository { get; }
        ISystemParametersRepository SystemParametersRepository { get; }
        IRegionRepository RegionRepository { get; }
        ICountryRepository CountryRepository { get; }
        IUserRepository UserRepository { get; }
        IUserCardGrantedRepository UserCardGrantedRepository { get; }
        IUserCardPermissionRepository UserCardPermissionRepository { get; }
        IRolesRepository RolesRepository { get; }
        IViewRepository ViewRepository { get; }
        ICatalogRepository CatalogRepository { get; }
        IViewCardRepository ViewCardRepository { get; }
        IUserViewRepository UserViewRepository { get; }
        ITableColumnRepository TableColumnRepository { get; }
        IEnterpriseHierarchyCatalogRepository EnterpriseHierarchyCatalogRepository { get; }
        ITreeRepository TreeRepository { get; }
        ICatalogTreeRepository CatalogTreeRepository { get; }
        IEnterpriseRepository EnterpriseRepository { get; }
        IPermissionsRepository PermissionsRepository { get; }
        ICardsRepository CardsRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}