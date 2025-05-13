using frontend.Data;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace frontend.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Subscription
        public IActionResult Index()
        {
            var subscriptions = _context.Subscriptions
                .Include(s => s.Offer)
                .OrderByDescending(s => s.CreatedAt)
                .ToList();

            return View("~/Views/Admin/Subscription/Index.cshtml", subscriptions);
        }

        // GET: /Subscription/Create
        public IActionResult Create()
        {
            ViewBag.Offers = _context.Offers.ToList();
            return View("~/Views/Admin/Subscription/Create.cshtml");
        }

        // POST: /Subscription/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Subscription subscription)
        {
            subscription.CreatedAt = DateTime.Now;
            subscription.UpdatedAt = DateTime.Now;
            subscription.StartDate = DateTime.Now;
            subscription.EndDate = subscription.StartDate.AddMonths(1);

            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Subscription created successfully!";
            return RedirectToAction("Index");
        }

        // GET: /Subscription/Edit/5
        public IActionResult Edit(int id)
        {
            var subscription = _context.Subscriptions.Find(id);
            if (subscription == null)
            {
                return NotFound();
            }

            ViewBag.Offers = _context.Offers.ToList();
            return View("~/Views/Admin/Subscription/Edit.cshtml", subscription);
        }

        // POST: /Subscription/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Subscription subscription)
        {
            var existing = _context.Subscriptions.Find(subscription.SubscriptionId);
            if (existing == null)
            {
                return NotFound();
            }

            existing.EmailList = subscription.EmailList;
            existing.OfferId = subscription.OfferId;
            existing.StartDate = subscription.StartDate;
            existing.EndDate = subscription.EndDate;
            existing.IsActive = subscription.IsActive;
            existing.PaymentVerified = subscription.PaymentVerified;
            existing.UpdatedAt = DateTime.Now;

            _context.Subscriptions.Update(existing);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Subscription updated successfully!";
            return RedirectToAction("Index");
        }

        // GET: /Subscription/Details/5
        public IActionResult Details(int id)
        {
            var subscription = _context.Subscriptions
                .Include(s => s.Offer)
                .FirstOrDefault(s => s.SubscriptionId == id);

            if (subscription == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Subscription/Details.cshtml", subscription);
        }

        // GET: /Subscription/Delete/5
        public IActionResult Delete(int id)
        {
            var subscription = _context.Subscriptions.Find(id);
            if (subscription == null)
            {
                return NotFound();
            }

            _context.Subscriptions.Remove(subscription);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Subscription deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
