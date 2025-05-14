using frontend.Models;
using frontend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace frontend.Controllers
{
    public class ECardTemplateController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public ECardTemplateController(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        // GET: ECardTemplate/Index
        public IActionResult Index()
        {
            var templates = _context.ECardTemplates.Include(e => e.Category).ToList();
            return View("~/Views/Admin/ECardTemplate/Index.cshtml", templates); // Explicit view path
        }

        // GET: ECardTemplate/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View("~/Views/Admin/ECardTemplate/Create.cshtml"); // Explicit view path
        }

        // POST: ECardTemplate/Create
        [HttpPost]
        public IActionResult Create(ECardTemplate template, IFormFile imageFile)
        {
            // Remove ModelState validation check

            if (imageFile != null)
            {
                string fileName = Path.GetFileName(imageFile.FileName);
                string filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fs);
                }
                template.ImageUrl = fileName;
            }

            template.CreatedAt = DateTime.Now;

            _context.ECardTemplates.Add(template);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: ECardTemplate/Edit/5
        public IActionResult Edit(int id)
        {
            var template = _context.ECardTemplates.Find(id);
            if (template == null) return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            return View("~/Views/Admin/ECardTemplate/Edit.cshtml", template); // Explicit view path
        }

        // POST: ECardTemplate/Edit/5
        [HttpPost]
        public IActionResult Edit(ECardTemplate template, IFormFile imageFile)
        {
            // Remove ModelState validation check

            var existingTemplate = _context.ECardTemplates.AsNoTracking().FirstOrDefault(t => t.TemplateId == template.TemplateId);
            if (existingTemplate == null) return NotFound();

            if (imageFile != null)
            {
                string fileName = Path.GetFileName(imageFile.FileName);
                string filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fs);
                }
                template.ImageUrl = fileName;
            }
            else
            {
                template.ImageUrl = existingTemplate.ImageUrl;
            }

            template.CreatedAt = existingTemplate.CreatedAt;

            _context.ECardTemplates.Update(template);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: ECardTemplate/Show/5
        public IActionResult Show(int id)
        {
            var template = _context.ECardTemplates
                .Include(t => t.Category)
                .FirstOrDefault(t => t.TemplateId == id);

            if (template == null) return NotFound();

            return View("~/Views/Admin/ECardTemplate/Show.cshtml", template); // Explicit view path
        }

        // GET: ECardTemplate/Delete/5
        public IActionResult Delete(int id)
        {
            var template = _context.ECardTemplates.Find(id);
            if (template == null) return NotFound();

            _context.ECardTemplates.Remove(template);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
