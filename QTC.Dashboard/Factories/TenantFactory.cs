using Dashboard.Common.Interfaces;
using QTC.Dashboard.WebApp.Interfaces;

namespace QTC.Dashboard.WebApp.Factories
{
    public class TenantFactory : ITenantFactory
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger _logger;

        public TenantFactory(ILogger<TenantFactory> logger, IServiceProvider provider)
        {
            _provider = provider;
            _logger = logger;
        }


        public ITenant CreateTenant(string name, string type)
        {
            var tenantName = type == "LIBRARY" ? name : "ServiceTenant";
            var tenant = _provider.GetServices<ITenant>().SingleOrDefault(x => x.Name.EndsWith(tenantName));
            if (tenant == null)
            {
                throw new Exception($"No ITenant service found with name ending with {tenantName}");
            }
            return tenant;
        }

        public async Task<string[]> GetTenantNamesAsync()
        {
            // Resolve ITenant[] from the IServiceProvider
            var tenants = _provider.GetServices<ITenant>();

            // Retrieve the names of the tenants
            var tenantNames = await Task.FromResult(tenants.Select(x => x.Name).ToArray());

            return tenantNames;
        }
    }
}
