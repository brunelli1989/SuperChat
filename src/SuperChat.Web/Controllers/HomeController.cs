using Microsoft.AspNetCore.Mvc;
using SuperChat.Web.Models;
using System.Diagnostics;

namespace SuperChat.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                return RedirectToAction("Index", "Groups");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
