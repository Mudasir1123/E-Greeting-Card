using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
