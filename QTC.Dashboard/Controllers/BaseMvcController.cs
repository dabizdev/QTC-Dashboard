//using Efm.Common.Entities;
//using Efm.Common.Modules;
//using Microsoft.AspNetCore.Mvc;
//using Qtc.Efm.BusinessLayer.AppBaseClasses;
//using Qtc.Efm.BusinessLayer.Common;
//using Qtc.Efm.BusinessLayer.EntityClasses;
//using Qtc.Efm.ViewModelLayer.ExamFileManager;
//using Qtc.Efm.WebApp.Common;
//using System.Reflection;

using Qtc.Dashboard.ViewModelLayer.Dashboard;
using Qtc.Dashboard.BusinessLayer.AppBaseClasses;
using Dashboard.Common.Modules;     
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Dashboard.Common.Interfaces;

namespace QTC.Dashboard.WebApp.Controllers
{
    public class BaseMvcController : Controller
    {
        protected readonly IConfiguration _config;
        protected readonly ILogger<BaseMvcController> _logger;
        public BaseMvcController(ILogger<BaseMvcController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public List<Assembly> GetAssemblies(string lob)
        {
            var assemblies = new List<Assembly>();

            var assemblyFilePath = Path.Combine(System.Environment.CurrentDirectory, "Assemblies", lob,
                $"{_config.GetValue<string>("EngineConfiguration:ClientSettings:Module")}.{lob}.dll");

            if (System.IO.File.Exists(assemblyFilePath))
            {
                var assembly = Assembly.LoadFile(assemblyFilePath);
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(IGetData)))
                    {
                        IEnumerable<Attribute> attrs = type.GetCustomAttributes(typeof(DashboardModuleAttribute));
                        if (attrs.Any())
                        {
                            DashboardModuleAttribute moduleAttr = attrs.ToArray()[0] as DashboardModuleAttribute;
                            if (moduleAttr?.Lob == lob)
                            {
                                assemblies.Add(assembly);
                                Console.WriteLine("Initializing module '{0}'.", moduleAttr?.Name);
                                break;
                            }
                        }
                    }
                }
            }

            return assemblies;
        }
        public IGetData GetErrors(string lob)
        {
            var collection = new ServiceCollection();
            var serviceType = _config.GetValue<string>("EngineConfiguration:ClientSettings:ServiceType");

            switch (serviceType)
            {
                case "LIBRARY":
                    // TODO: Would like to clean this up. Currently
                    // TODO: the algorithm searches the folder for all assemblies that contain the client's organization and loads them
                    // The intent is to load a single assembly that represents the business logic for
                    // the organization but the current method has the following issues:
                    // 1. There could be more than one matching assembly
                    // 2. Matching assemblies are put into a list and all implementations of the
                    //    relevant interfaces from all assemblies are registered with the service provider
                    //    This may cause issues when trying to identify the correct service to handle a call later.

                    var assemblies = GetAssemblies(lob);

                    collection.Scan(scan => scan
                                    .FromAssemblies(assemblies)
                                    .AddClasses(classes => classes.AssignableTo<IGetData>(), publicOnly: true)
                                    .AsImplementedInterfaces()
                                    .WithTransientLifetime());


                   // collection.Scan(scan => scan
                   //     .FromAssemblies(assemblies)
                   //     .AddClasses(classes => classes.AssignableTo<IDashboardModule>(), publicOnly: true)
                   //     .AsImplementedInterfaces()
                   //     .WithTransientLifetime());

                    break;
                case "SERVICE":
                    break;
                default:
                    break;
            }

            var serviceProvider = collection.BuildServiceProvider();

            var errorsModule = serviceProvider.GetService<IGetData>();

            return errorsModule;
        }

        public void SetMVCCommonViewModelProperties(DashboardViewModel vm)
        {
            vm.ShowSpinner = TempData["ShowSpinner"] as bool? ?? true;
            if (vm.ShowSpinner.HasValue && !vm.ShowSpinner.Value)
            {
                vm.Messages = TempData["Messages"] as List<string> ?? new List<string>();
                vm.Message = TempData["Message"] as string ?? String.Empty;
            }
        }

        public void GetUserPermissions(DashboardViewModel vm)
        {
            //vm.Permissions = AuthPermissions.GetUserPermissions(User.ToString());
        }

        public void SetViewModelMessages(DashboardViewModel vm)
        {
            if (vm.Messages != null && vm.Messages.Count > 0)
            {
                TempData["Messages"] = vm.Messages;
            }

            if (!String.IsNullOrEmpty(vm.Message))
            {
                TempData["Message"] = vm.Message;
            }

        }


    }
}
