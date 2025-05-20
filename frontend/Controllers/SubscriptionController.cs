//using frontend.Data;
//using frontend.Migrations;
//using frontend.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;

//namespace frontend.Controllers
//{
//    public class SubscriptionController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public SubscriptionController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: /Subscription
//        public IActionResult Index()
//        {
//            var subscriptions = _context.Subscriptions
//                .Include(s => s.Plan)
//                .Include(s => s.Offer)
//                .OrderByDescending(s => s.StartDate)
//                .ToList();

//            return View("~/Views/Admin/Subscription/Index.cshtml", subscriptions);
//        }

//        // GET: /Subscription/Create
//        public IActionResult Create()
//        {
//            ViewBag.Plans = _context.Plans.Where(p => p.IsActive).ToList();
//            ViewBag.Offers = _context.Offers
//                .Where(o => o.IsActive && o.StartDate <= DateTime.Now && o.EndDate >= DateTime.Now)
//                .ToList();

//            return View("~/Views/Admin/Subscription/Create.cshtml");
//        }

//        // POST: /Subscription/Create
//        [HttpPost]
//        public IActionResult Create(Subscription subscription)
//        {
//            var plan = _context.Plans.Find(subscription.PlanId);
//            if (plan == null) return NotFound();

//            subscription.StartDate = DateTime.Now;
//            subscription.EndDate = DateTime.Now.AddMonths(plan.DurationInMonths);
//            subscription.Amount = plan.Price;
//            subscription.FinalAmount = plan.Price;

//            if (subscription.OfferId.HasValue)
//            {
//                var offer = _context.Offers.Find(subscription.OfferId);
//                if (offer != null)
//                {
//                    subscription.DiscountAmount = plan.Price * (offer.DiscountPercentage / 100);
//                    subscription.FinalAmount = plan.Price - subscription.DiscountAmount;
//                }
//            }

//            _context.Subscriptions.Add(subscription);
//            _context.SaveChanges();

//            return RedirectToAction("Index");
//        }

//        // GET: /Subscription/Edit/5
//        public IActionResult Edit(int id)
//        {
//            var subscription = _context.Subscriptions
//                .Include(s => s.Plan)
//                .Include(s => s.Offer)
//                .FirstOrDefault(s => s.Id == id);

//            if (subscription == null) return NotFound();

//            ViewBag.Plans = _context.Plans.ToList();
//            ViewBag.Offers = _context.Offers.ToList();

//            return View("~/Views/Admin/Subscription/Edit.cshtml", subscription);
//        }

//        // POST: /Subscription/Edit/5
//        [HttpPost]
//        public IActionResult Edit(Subscription subscription)
//        {
//            var existing = _context.Subscriptions.Find(subscription.Id);
//            if (existing == null) return NotFound();

//            existing.PlanId = subscription.PlanId;
//            existing.OfferId = subscription.OfferId;
//            existing.IsActive = subscription.IsActive;
//            existing.UpdatedAt = DateTime.Now;

//            _context.Subscriptions.Update(existing);
//            _context.SaveChanges();

//            return RedirectToAction("Index");
//        }

//        // POST: /Subscription/Cancel/5
//        [HttpPost]
//        public IActionResult Cancel(int id)
//        {
//            var subscription = _context.Subscriptions.Find(id);
//            if (subscription == null) return NotFound();

//            subscription.IsActive = false;
//            subscription.CancelledDate = DateTime.Now;
//            subscription.UpdatedAt = DateTime.Now;

//            _context.Subscriptions.Update(subscription);
//            _context.SaveChanges();

//            return RedirectToAction("Index");
//        }

//        // GET: /Subscription/Details/5
//        public IActionResult Details(int id)
//        {
//            var subscription = _context.Subscriptions
//                .Include(s => s.Plan)
//                .Include(s => s.Offer)
//                .FirstOrDefault(s => s.Id == id);

//            return subscription == null
//                ? NotFound()
//                : View("~/Views/Admin/Subscription/Details.cshtml", subscription);
//        }
//    }
//}