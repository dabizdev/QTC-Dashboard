using Microsoft.AspNetCore.Mvc;

namespace QTC.Dashboard.WebApp.Controllers
{
    public class ErrorTableController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Headers"] = new string[] { "Application Name", "Layer", "Module", "Alert", "AlertTeam", "Severity", "ServerName", "ErrorCode", "Error Message", "Error Date", "User", "View" };
            return View();
        }
    }
}
