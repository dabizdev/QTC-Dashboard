using Dashboard.Common.Interfaces;
using QTC.Dashboard.WebApp.Interfaces;

namespace QTC.Dashboard.WebApp.Factories
{
    public class GetDataFactory : IGetDataFactory
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger _logger;

        public GetDataFactory(ILogger<TenantFactory> logger, IServiceProvider provider)
        {
            _provider = provider;
            _logger = logger;
        }

        public IGetData CreateGetData(string name, string type)
        {
            var tenantName = type == "LIBRARY" ? name : "ServiceTenant";
            var getData = _provider.GetServices<IGetData>().SingleOrDefault(x => x.Name.EndsWith(tenantName));
            return getData;
        }
    }
}
