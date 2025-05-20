using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Dashboard() => View();

        public IActionResult CardManagement() => View();

        public IActionResult UserManagement() => View();

        public IActionResult Categories() => View();

        public IActionResult Templates() => View();

        public IActionResult Subscriptions() => View();

        public IActionResult Analytics() => View();

        public IActionResult Settings() => View();
    }
}
