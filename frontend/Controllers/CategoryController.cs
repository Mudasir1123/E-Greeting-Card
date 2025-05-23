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
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Category/Edit.cshtml", category);
        }

        // Handle Edit POST without validation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            category.UpdatedAt = DateTime.Now;
            _context.Categories.Update(category);
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

    }
}
