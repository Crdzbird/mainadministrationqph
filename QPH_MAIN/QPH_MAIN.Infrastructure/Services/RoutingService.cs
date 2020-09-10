using Microsoft.Extensions.Options;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Infrastructure.Interfaces;

namespace QPH_MAIN.Infrastructure.Services
{
    public class RoutingService : IRoutingService
    {
        private readonly CustomRoutes _options;
        public RoutingService(IOptions<CustomRoutes> options)
        {
            _options = options.Value;
        }
        public string GetRoute() => (_options.route != "") ? $"{_options.route}/" : "";
    }
}