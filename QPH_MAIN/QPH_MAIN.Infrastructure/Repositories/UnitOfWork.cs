using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QPHContext _context;
        private readonly ICityRepository _cityRepository;
        private readonly ICardsRepository _cardsRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IViewRepository _viewsRepository;
        private readonly IUserViewRepository _userViewRepository;
        private readonly IUserCardGrantedRepository _userCardGrantedRepository;
        private readonly ITreeRepository _treeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly IPermissionsRepository _permissionsRepository;

        public UnitOfWork(QPHContext context)
        {
            _context = context;
        }

        public ICityRepository CityRepository => _cityRepository ?? new CityRepository(_context);
        public ICardsRepository CardsRepository => _cardsRepository ?? new CardsRepository(_context);
        public IRegionRepository RegionRepository => _regionRepository ?? new RegionRepository(_context);
        public ICountryRepository CountryRepository => _countryRepository ?? new CountryRepository(_context);
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        public IUserCardGrantedRepository UserCardGrantedRepository => _userCardGrantedRepository ?? new UserCardGrantedRepository(_context);
        public IRolesRepository RolesRepository => _rolesRepository ?? new RolesRepository(_context);
        public IViewRepository ViewRepository => _viewsRepository ?? new ViewsRepository(_context);
        public ITreeRepository TreeRepository => _treeRepository ?? new TreeRepository(_context);
        public IUserViewRepository UserViewRepository => _userViewRepository ?? new UserViewRepository(_context);
        public IEnterpriseRepository EnterpriseRepository => _enterpriseRepository ?? new EnterpriseRepository(_context);
        public IPermissionsRepository PermissionsRepository => _permissionsRepository ?? new PermissionsRepository(_context);

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }

        public void SaveChanges() => _context.SaveChanges();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}