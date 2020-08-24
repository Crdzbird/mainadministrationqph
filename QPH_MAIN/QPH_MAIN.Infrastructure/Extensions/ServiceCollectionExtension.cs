using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.Services;
using QPH_MAIN.Infrastructure.Data;
using QPH_MAIN.Infrastructure.Interfaces;
using QPH_MAIN.Infrastructure.Options;
using QPH_MAIN.Infrastructure.Repositories;
using QPH_MAIN.Infrastructure.Services;
using System;
using System.IO;

namespace QPH_MAIN.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QPHContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("RRHH_MAIN"))
           );
            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));
            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IEnterpriseService, EnterpriseService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IViewService, ViewService>();
            services.AddTransient<IUserViewService, HierarchyViewService>();
            services.AddTransient<ITreeService, TreeService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IPermissionsService, PermissionsService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFileName)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "QPH API", Version = "v1" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                doc.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}