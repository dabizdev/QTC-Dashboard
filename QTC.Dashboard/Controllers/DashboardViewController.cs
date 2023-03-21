using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Qtc.Dashboard.ViewModelLayer.Dashboard;
using QTC.Dashboard.WebApp.Factories;
using QTC.Dashboard.WebApp.Interfaces;
using System.Reflection.Metadata;

namespace QTC.Dashboard.WebApp.Controllers
{
    public class DashboardViewController : BaseMvcController
    {
        private readonly ITenantFactory _tenantFactory;
        private readonly IErrorTypeModuleFactory _errorTypeModuleFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<DashboardViewModel> _logger;

        public DashboardViewController(ILogger<DashboardViewController> logger,
                                            IConfiguration config, ITenantFactory tenantFactory, IErrorTypeModuleFactory errorTypeModuleFactory)
        : base(logger, config)
        {
            //_logger = logger;
            _config = config;
            _tenantFactory = tenantFactory;
            _errorTypeModuleFactory = errorTypeModuleFactory;
        }
        public async Task<IActionResult> Index(string lob)
        {
            try
            {
                var vm = new DashboardViewModel(lob, "list");
                // create a new instance of Dashboard View Model and instantiate with default vals in Init() method
                //var vm = new DashboardViewModel();
                //vm.Init();

                SetMVCCommonViewModelProperties(vm);

                // this shows a spinning logo while the information is being loaded
                if (vm.ShowSpinner.HasValue && vm.ShowSpinner.Value)
                {
                    return View("~/Views/Shared/ShowSpinner.cshtml");
                }

                vm.SetTenant(_tenantFactory.CreateTenant(lob, "LIBRARY"));
                //vm.SetErrorTypeModule(vm.GetTenant().GetErrorTypeProcessor());
                //var data = this.GetErrors(lob);
                //GetUserPermissions(vm);

                ////Don't allow access for event referrals to inclinic page
                //if (!vm.Permissions.HasMedicalRecordsAccess)
                //{
                //    return View("~/Views/Shared/Unauthorized.cshtml");
                //}

                vm.HandleRequest();

                // redirect user to errortable view under "views folder"
                return View("Home", vm);
            }
            catch (Exception e)
            {
                // re-throw cause the error page to display
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DashboardViewModel vm)
        {
            ActionResult ret = View(vm);

            try
            {
                //vm.SetEngineManager(_engineManager);
                SetMVCCommonViewModelProperties(vm);
                //vm.SetMedicalRecordsModule(GetMedicalRecordsModule(vm.Lob));

                vm.SetTenant(_tenantFactory.CreateTenant(vm.Lob, "LIBRARY"));
                //vm.SetGetData(_errorTypeModuleFactory.CreateErrorTypeModule("integration point"));
                GetUserPermissions(vm);

                //Validation for data annotations
                if (!ModelState.IsValid)
                {
                    //return the same view in order not to lose the data from the user, but
                    //not necessary for this page
                    foreach (var modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            //vm.Messages.Add(error.ErrorMessage);
                        }
                    }
                }

                // Clear the model state to bind the @Html helpers to new model values
                ModelState.Clear();

                //Handle the request from the user
                vm.HandleRequest();

                //Set errors or messages
                SetViewModelMessages(vm);

                // Handle any MVC actions to take in case it needs to be redirected to a different controller
                switch (vm.EventAction)
                {
                    default:
                        //ret = RedirectToAction("Edit", "ExamFileManager", new { lob = vm.Lob });
                        break;
                }
            }
            catch (Exception e)
            {
                // re-throw cause the error page to display
                ret = RedirectToAction("Index", "Home", new { lob = vm.Lob });
            }

            return ret;
        }
    }
}
