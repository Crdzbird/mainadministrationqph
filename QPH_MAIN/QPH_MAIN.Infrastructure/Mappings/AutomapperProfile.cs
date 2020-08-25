using AutoMapper;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<Region, RegionDto>();
            CreateMap<RegionDto, Region>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Roles, RolesDto>();
            CreateMap<RolesDto, Roles>();
            CreateMap<Views, ViewsDto>();
            CreateMap<ViewsDto, Views>();
            CreateMap<Tree, TreeDto>();
            CreateMap<TreeDto, Tree>();
            CreateMap<UserView, UserViewDto>();
            CreateMap<UserViewDto, UserView>();
            CreateMap<Enterprise, EnterpriseDto>();
            CreateMap<EnterpriseDto, Enterprise>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<PermissionsDto, Permissions>();
            CreateMap<Permissions, PermissionsDto>();
            CreateMap<CardsDto, Cards>();
            CreateMap<Cards, CardsDto>();
            CreateMap<Security, SecurityDto>().ReverseMap();
        }
    }
}