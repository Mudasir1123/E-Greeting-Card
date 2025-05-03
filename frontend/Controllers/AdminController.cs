using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult CardManagement()
        {
            return View();
        }

        public IActionResult UserManagement()
        {
            return View();
        }

        public IActionResult Categories()
        {
            return View();
        }

        public IActionResult Templates()
        {
            return View();
        }

        public IActionResult Subscriptions()
        {
            return View();
        }

        public IActionResult Analytics()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
