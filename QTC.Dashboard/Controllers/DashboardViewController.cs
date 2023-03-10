using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using QTC.Dashboard.WebApp.Interfaces;
using Qtc.Dashboard.ViewModelLayer.Dashboard;

namespace QTC.Dashboard.WebApp.Controllers
{
    public class DashboardViewController : BaseMvcController
    {
        private readonly ITenantFactory _tenantFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<DashboardViewController> _logger;


        public DashboardViewController(ILogger<DashboardViewController> logger, IConfiguration config, ITenantFactory tenantFactory)
            : base(logger, config)
        {
            _logger = logger;
            _config = config;
            _tenantFactory = tenantFactory;
        }

        public async Task<IActionResult> Index(string lob)
        {
            try
            {
                var vm = new DashboardViewModel();
                vm.Init();
                SetMVCCommonViewModelProperties(vm);

                if (vm.ShowSpinner.HasValue && vm.ShowSpinner.Value)
                {
                    return View("~/Views/Shared/ShowSpinner.cshtml");
                }

                var tenantNames = (await _tenantFactory.GetTenantNamesAsync()).ToList();

                vm.TenantNames = tenantNames;

                return View("Home", vm);
            }
            catch (Exception e)
            {
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
                SetMVCCommonViewModelProperties(vm);
                Task<string[]> tenantNames = (_tenantFactory.GetTenantNamesAsync());

                vm.TenantNames = (IEnumerable<string>)tenantNames;

                //await GetUserPermissionsAsync(vm);

                if (!ModelState.IsValid)
                {
                    foreach (var modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            //vm.Messages.Add(error.ErrorMessage);
                        }
                    }
                }

                ModelState.Clear();

                vm.HandleRequest();

                SetViewModelMessages(vm);

                switch (vm.EventAction)
                {
                    default:
                        //ret = RedirectToAction("Edit", "ExamFileManager", new { lob = vm.Lob });
                        break;
                }
            }
            catch (Exception e)
            {
                ret = RedirectToAction("Index", "Home", new { lob = vm.Lob });
            }

            return ret;
        }
    }
}