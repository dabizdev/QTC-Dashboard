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


        public IGetData CreateTenant(string name, string type)
        {
            
            //Console.WriteLine(_provider.GetServices<IGetData>());
            var tenantName = type == "LIBRARY" ? name : "ServiceTenant";
            var tenant = _provider.GetServices<IGetData>().SingleOrDefault(x => x.Name.EndsWith(tenantName));
            return tenant;
        }


    }
}
