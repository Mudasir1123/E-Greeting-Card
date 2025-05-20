//using EGreeting.Models;
//using frontend.Data;
//using frontend.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Linq;

//namespace frontend.Controllers
//{
//    public class OfferController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public OfferController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: /Offer
//        public IActionResult Index()
//        {
//            var offers = _context.Offers
//                .OrderByDescending(o => o.IsActive)
//                .ThenByDescending(o => o.CreatedAt)
//                .ToList();

//            return View("~/Views/Admin/Offer/Index.cshtml", offers);
//        }

//        // GET: /Offer/Create
//        public IActionResult Create()
//        {
//            var offer = new Offer
//            {
//                StartDate = DateTime.Now,
//                EndDate = DateTime.Now.AddMonths(1),
//                IsActive = true,
//                CreatedAt = DateTime.Now,
//                UpdatedAt = DateTime.Now
//            };

//            return View("~/Views/Admin/Offer/Create.cshtml", offer);
//        }

//        // POST: /Offer/Create
//        [HttpPost]
//        public IActionResult Create(Offer offer)
//        {
//            offer.CreatedAt = DateTime.Now;
//            offer.UpdatedAt = DateTime.Now;

//            _context.Offers.Add(offer);
//            _context.SaveChanges();

//            return RedirectToAction("Index");
//        }

//        // GET: /Offer/Edit/5
//        public IActionResult Edit(int id)
//        {
//            var offer = _context.Offers.Find(id);
//            return offer == null
//                ? NotFound()
//                : View("~/Views/Admin/Offer/Edit.cshtml", offer);
//        }

//        // POST: /Offer/Edit/5
//        [HttpPost]
//        public IActionResult Edit(Offer offer)
//        {
//            offer.UpdatedAt = DateTime.Now;
//            _context.Offers.Update(offer);
//            _context.SaveChanges();

//            return RedirectToAction("Index");
//        }

//        // POST: /Offer/Delete/5
//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            var offer = _context.Offers.Find(id);
//            if (offer == null) return NotFound();

//            if (offer.UsedCount > 0)
//            {
//                offer.IsActive = false;
//                _context.Offers.Update(offer);
//            }
//            else
//            {
//                _context.Offers.Remove(offer);
//            }

//            _context.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        // POST: /Offer/ToggleActive/5
//        [HttpPost]
//        public IActionResult ToggleActive(int id)
//        {
//            var offer = _context.Offers.Find(id);
//            if (offer == null) return NotFound();

//            offer.IsActive = !offer.IsActive;
//            offer.UpdatedAt = DateTime.Now;
//            _context.Offers.Update(offer);
//            _context.SaveChanges();

//            return RedirectToAction("Index");
//        }
//    }
//}