using Dashboard.Common.DataModels;

namespace QTC.Dashboard.WebApp.Interfaces
{
    public interface IErrorTypeModuleFactory
    {
        public List<Errors> GetData(string integrationPoint);
    }
}
