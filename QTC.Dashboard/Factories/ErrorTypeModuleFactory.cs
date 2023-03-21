using Dashboard.Common.Interfaces;
using QTC.Dashboard.WebApp.Interfaces;

namespace QTC.Dashboard.WebApp.Factories
{
    public class ErrorTypeModuleFactory: IErrorTypeModuleFactory
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger _logger;

        public ErrorTypeModuleFactory(ILogger<ErrorTypeModuleFactory> logger, IServiceProvider provider)
        {
            _provider = provider;
            _logger = logger;
        }


        public IErrorTypeModule CreateErrorTypeModule(string integrationPoint)
        {
            var errorTypeModule = _provider.GetRequiredService<IErrorTypeModule>();

            return errorTypeModule;
            
        }

    }
}
