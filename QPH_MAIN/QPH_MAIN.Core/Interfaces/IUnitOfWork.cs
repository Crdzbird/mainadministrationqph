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
        IUserCardGrantedRepository UserCardGrantedRepository { get; }
        IRolesRepository RolesRepository { get; }
        IViewRepository ViewRepository { get; }
        IViewCardRepository ViewCardRepository { get; }
        IUserViewRepository UserViewRepository { get; }
        ITreeRepository TreeRepository { get; }
        IEnterpriseRepository EnterpriseRepository { get; }
        IPermissionsRepository PermissionsRepository { get; }
        ICardsRepository CardsRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}