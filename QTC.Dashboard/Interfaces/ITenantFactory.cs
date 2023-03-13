using Dashboard.Common.Interfaces;

namespace QTC.Dashboard.WebApp.Interfaces
{
    public interface ITenantFactory
    {
        public IGetData CreateTenant(string name, string type);
    }
}
