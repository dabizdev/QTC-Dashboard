using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using QTC.Dashboard.WebApp.Interfaces;
using Qtc.Dashboard.ViewModelLayer.Dashboard;

namespace QTC.Dashboard.WebApp.Controllers
{
    public class DashboardController : BaseMvcController
    {
        private readonly ITenantFactory _tenantFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<DashboardViewModel> _logger;

        public DashboardController(ILogger<DashboardController> logger,
                                            IConfiguration config, ITenantFactory tenantFactory)
        : base(logger, config)
        {
            _logger = (ILogger<DashboardViewModel>?)logger;
            _config = config;
            _tenantFactory = tenantFactory;
        }
        public async Task<IActionResult> Errors(string lob)
        {
            try
            {
                var vm = new DashboardViewModel(lob, "list");

                SetMVCCommonViewModelProperties(vm);

                if (vm.ShowSpinner.HasValue && vm.ShowSpinner.Value)
                {
                    return View("~/Views/Shared/ShowSpinner.cshtml");
                }

                vm.SetTenant(_tenantFactory.CreateTenant(lob, "LIBRARY"));
                GetUserPermissions(vm);

                ////Don't allow access for event referrals to inclinic page
                //if (!vm.Permissions.HasMedicalRecordsAccess)
                //{
                //    return View("~/Views/Shared/Unauthorized.cshtml");
                //}

                vm.HandleRequest();

                return View("Errors", vm);
            }
            catch (Exception e)
            {
                // re-throw cause the error page to display

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Errors(DashboardViewModel vm)
        {
            ActionResult ret = View(vm);

            try
            {
                //vm.SetEngineManager(_engineManager);
                SetMVCCommonViewModelProperties(vm);
                //vm.SetMedicalRecordsModule(GetMedicalRecordsModule(vm.Lob));
                vm.SetTenant(_tenantFactory.CreateTenant(vm.Lob, "LIBRARY"));
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
                ret = RedirectToAction("Errors", "Dashboard", new { lob = vm.Lob });
            }

            return ret;
        }
    }
}
