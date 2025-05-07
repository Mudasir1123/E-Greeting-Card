using Microsoft.AspNetCore.Mvc;
namespace frontend.Controllers
{

    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
