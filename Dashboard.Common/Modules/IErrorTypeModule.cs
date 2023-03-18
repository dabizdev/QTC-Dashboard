using Dashboard.Common.DataModels;

namespace Dashboard.Common.Modules
{
    public interface IErrorTypeModule
    {
        List<Errors> GetData(string integrationPoint);
    }
}
