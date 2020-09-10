using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QPHContext _context;
        private readonly ICityRepository _cityRepository;
        private readonly ISystemParametersRepository _systemParametersRepository;
        private readonly ITableColumnRepository _tableColumnRepository;
        private readonly ICardsRepository _cardsRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IViewRepository _viewsRepository;
        private readonly IUserViewRepository _userViewRepository;
        private readonly IViewCardRepository _viewCardRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IEnterpriseHierarchyCatalogRepository _enterpriseHierarchyCatalogRepository;
        private readonly IUserCardGrantedRepository _userCardGrantedRepository;
        private readonly IUserCardPermissionRepository _userCardPermissionRepository;
        private readonly ITreeRepository _treeRepository;
        private readonly ICatalogTreeRepository _catalogTreeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IBlacklistRepository _blacklistRepository;

        public UnitOfWork(QPHContext context)
        {
            _context = context;
        }

        public ICityRepository CityRepository => _cityRepository ?? new CityRepository(_context);
        public ITableColumnRepository TableColumnRepository => _tableColumnRepository ?? new TableColumnRepository(_context);
        public ISystemParametersRepository SystemParametersRepository => _systemParametersRepository ?? new SystemParametersRepository(_context);
        public ICardsRepository CardsRepository => _cardsRepository ?? new CardsRepository(_context);
        public IRegionRepository RegionRepository => _regionRepository ?? new RegionRepository(_context);
        public ICountryRepository CountryRepository => _countryRepository ?? new CountryRepository(_context);
        public ICatalogRepository CatalogRepository => _catalogRepository ?? new CatalogRepository(_context);
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        public IEnterpriseHierarchyCatalogRepository EnterpriseHierarchyCatalogRepository => _enterpriseHierarchyCatalogRepository ?? new EnterpriseHierarchyCatalogRepository(_context);
        public IUserCardGrantedRepository UserCardGrantedRepository => _userCardGrantedRepository ?? new UserCardGrantedRepository(_context);
        public IUserCardPermissionRepository UserCardPermissionRepository => _userCardPermissionRepository ?? new UserCardPermissionRepository(_context);
        public IRolesRepository RolesRepository => _rolesRepository ?? new RolesRepository(_context);
        public IViewRepository ViewRepository => _viewsRepository ?? new ViewsRepository(_context);
        public IViewCardRepository ViewCardRepository => _viewCardRepository ?? new ViewCardRepository(_context);
        public ITreeRepository TreeRepository => _treeRepository ?? new TreeRepository(_context);
        public ICatalogTreeRepository CatalogTreeRepository => _catalogTreeRepository ?? new CatalogTreeRepository(_context);
        public IUserViewRepository UserViewRepository => _userViewRepository ?? new UserViewRepository(_context);
        public IEnterpriseRepository EnterpriseRepository => _enterpriseRepository ?? new EnterpriseRepository(_context);
        public IPermissionsRepository PermissionsRepository => _permissionsRepository ?? new PermissionsRepository(_context);
        public IBlacklistRepository BlacklistRepository => _blacklistRepository ?? new BlacklistRepository(_context);

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }

        public void SaveChanges() => _context.SaveChanges();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}