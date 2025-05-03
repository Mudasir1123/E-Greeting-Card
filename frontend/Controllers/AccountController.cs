using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // GET: /Account/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        // GET: /Account/ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}