using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class SubscriptionController : Controller
    {
        public IActionResult Index(string plan = "free")
        {
            ViewBag.SelectedPlan = plan; // Pass the plan to the view
            return View();
        }
    }
}