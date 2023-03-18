using Dashboard.Common.DataModels;

namespace Dashboard.Common.Interfaces
{
    public interface IErrorTypeModule
    {
        public List<Errors> GetData(string integrationPoint);
    }
}
