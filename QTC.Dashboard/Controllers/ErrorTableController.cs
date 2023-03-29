using Dashboard.Common.DataModels;
using Dashboard.Common.DataModels.ControllerModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Qtc.Dashboard.ViewModelLayer.Dashboard;
using QTC.Dashboard.WebApp.Interfaces;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace QTC.Dashboard.WebApp.Controllers
{
    public class ErrorTableController : BaseMvcController
    {
        private readonly ITenantFactory _tenantFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<DashboardViewModel> _logger;
        public readonly string[] neededHeaders;

        public ErrorTableController(ILogger<ErrorTableController> logger,
                                            IConfiguration config, ITenantFactory tenantFactory)
        : base(logger, config)
        {
            //_logger = logger;
            _config = config;
            _tenantFactory = tenantFactory;
            
        }

        // takes in the org and application that is sent to the page when user clicks on specific application
        public IActionResult Index(string lob)
        {
            try
            {
                var vm = new DashboardViewModel(lob, "list");
                // create a new instance of Dashboard View Model and instantiate with default vals in Init() method
                //var vm = new DashboardViewModel();
                vm.Init();

                ViewData["LobName"] = lob;

                SetMVCCommonViewModelProperties(vm);

                // this shows a spinning logo while the information is being loaded
                if (vm.ShowSpinner.HasValue && vm.ShowSpinner.Value)
                {
                    return View("~/Views/Shared/ShowSpinner.cshtml");
                }

                vm.SetTenant(_tenantFactory.CreateTenant(lob, "LIBRARY"));

                // create an array of strings with the headers needed
                var neededHeaders = new string[] { "Application Name", "Layer", "Module", "Alert", "AlertTeam", "Severity", "ServerName", "ErrorCode", "Error Message", "Error Date", "User", "View" };
                vm.headers = neededHeaders.ToList();
                //GetUserPermissions(vm);

                ////Don't allow access for event referrals to inclinic page
                //if (!vm.Permissions.HasMedicalRecordsAccess)
                //{
                //    return View("~/Views/Shared/Unauthorized.cshtml");
                //}

                vm.HandleRequest();

                // redirect user to errortable view under "views folder"
                return View(vm);
            }
            catch (Exception e)
            {
                // re-throw cause the error page to display
                throw;
            }
        }
    }
}
