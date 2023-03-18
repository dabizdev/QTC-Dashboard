using Dashboard.Common.DataModels;
using Dashboard.Common.Interfaces;

namespace QTC.Dashboard.WebApp.Interfaces
{
    public interface IErrorTypeModuleFactory
    {
        public IErrorTypeModule CreateErrorTypeModule(string integrationPoint);
    }
}
