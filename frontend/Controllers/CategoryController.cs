using frontend.Data;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View("~/Views/Admin/category/Index.cshtml", categories);
        }

        public IActionResult Create()
        {
            return View("~/Views/Admin/category/create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = DateTime.Now;
            _context.Add(category);
             _context.SaveChanges();
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

            ViewBag.Offers = _context.Offers.ToList(); // Optional: for dropdown
            return View("~/Views/Admin/Subscription/Edit.cshtml", subscription);
        }

        // POST: /Subscription/Edit
        [HttpPost]
        public IActionResult Edit(Subscription subscription)
        {
            // Direct update like Category example
            subscription.UpdatedAt = DateTime.Now;
            _context.Subscriptions.Update(subscription);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
      

        public IActionResult Details(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //public IActionResult ToggleActive(int id)
        //{
        //    var category = _context.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    category.IsActive = !category.IsActive;
        //    _context.Categories.Update(category);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}
    }
}
