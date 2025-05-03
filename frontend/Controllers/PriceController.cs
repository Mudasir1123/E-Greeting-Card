using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class PriceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
