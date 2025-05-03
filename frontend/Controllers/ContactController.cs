using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
