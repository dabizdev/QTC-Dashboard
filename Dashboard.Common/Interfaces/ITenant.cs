using Dashboard.Common.Modules;

namespace Dashboard.Common.Interfaces
{
    public interface ITenant
    {
        public string Name { get; }

        IErrorTypeModule GetErrorTypeProcessor();

    }
}
