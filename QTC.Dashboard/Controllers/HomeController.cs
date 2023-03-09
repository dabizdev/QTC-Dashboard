using Microsoft.AspNetCore.Mvc;
using Qtc.Dashboard.ViewModelLayer.Dashboard;
using QTC.Dashboard.Models;
using System.Diagnostics;

namespace QTC.Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var vm = new DashboardViewModel();
            vm.Init();

            //SetMVCCommonViewModelProperties(vm);

            // this shows a spinning logo while the information is being loaded
            if (vm.ShowSpinner.HasValue && vm.ShowSpinner.Value)
            {
                return View("~/Views/Shared/ShowSpinner.cshtml");
            }

            // redirect user to errortable view under "views folder"
            return View(vm);
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}