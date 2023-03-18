using Dashboard.Common.Interfaces;

namespace QTC.Dashboard.WebApp.Interfaces
{
    public interface IGetDataFactory
    {
        public IGetData CreateGetData(string name, string type);
    }
}
