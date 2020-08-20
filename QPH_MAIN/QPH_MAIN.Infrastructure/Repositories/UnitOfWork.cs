using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QPHContext _context;
        private readonly ICityRepository _cityRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IViewRepository _viewsRepository;
        private readonly IUserViewRepository _userViewRepository;
        private readonly ITreeRepository _treeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly ISecurityRepository _securityRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;

        public UnitOfWork(QPHContext context)
        {
            _context = context;
        }

        public ICityRepository CityRepository => _cityRepository ?? new CityRepository(_context);
        public IRegionRepository RegionRepository => _regionRepository ?? new RegionRepository(_context);
        public ICountryRepository CountryRepository => _countryRepository ?? new CountryRepository(_context);
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        public ISecurityRepository SecurityRepository => _securityRepository ?? new SecurityRepository(_context);
        public IRolesRepository RolesRepository => _rolesRepository ?? new RolesRepository(_context);
        public IViewRepository ViewRepository => _viewsRepository ?? new ViewsRepository(_context);
        public ITreeRepository TreeRepository => _treeRepository ?? new TreeRepository(_context);
        public IUserViewRepository UserViewRepository => _userViewRepository ?? new UserViewRepository(_context);
        public IEnterpriseRepository EnterpriseRepository => _enterpriseRepository ?? new EnterpriseRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
