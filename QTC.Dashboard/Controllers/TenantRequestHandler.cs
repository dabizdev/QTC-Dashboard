
using System.Collections.Generic;
using Qtc.Dashboard.ViewModelLayer.Dashboard;
using QTC.Dashboard.WebApp.Interfaces;

namespace QTC.Dashboard.WebApp.Controllers
{
    public class TenantRequestHandler : ITenantRequestHandler
    {
        private readonly ITenantFactory _tenantService;

        public TenantRequestHandler(ITenantFactory tenantService)
        {
            _tenantService = tenantService;
        }

        public Task<string[]> GetTenantNamesAsync()
        {
            return _tenantService.GetTenantNamesAsync();
        }
    }
}
