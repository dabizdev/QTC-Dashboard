using Dashboard.Common.Interfaces;

namespace QTC.Dashboard.WebApp.Interfaces
{
    public interface ITenantFactory
    {
        public ITenant CreateTenant(string name, string type);
    }
}
