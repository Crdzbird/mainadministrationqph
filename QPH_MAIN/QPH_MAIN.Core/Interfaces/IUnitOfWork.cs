using System;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository CityRepository { get; }
        IRegionRepository RegionRepository { get; }
        ICountryRepository CountryRepository { get; }
        IUserRepository UserRepository { get; }
        IRolesRepository RolesRepository { get; }
        IViewRepository ViewRepository { get; }
        IUserViewRepository UserViewRepository { get; }
        ITreeRepository TreeRepository { get; }
        IEnterpriseRepository EnterpriseRepository { get; }
        IPermissionsRepository PermissionsRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}