using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using frontend.Data;
using frontend.Models;

namespace frontend.Controllers
{
    public class ECardTemplateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ECardTemplateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ECardTemplate
        public async Task<IActionResult> Index()
        {
            var templates = await _context.ECardTemplates
                .Include(t => t.Category)
                .ToListAsync();
            return View(templates);
        }

        // GET: ECardTemplate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.ECardTemplates
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TemplateId == id);

            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // GET: ECardTemplate/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: ECardTemplate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ImageUrl,CategoryId")] ECardTemplate template)
        {
            if (ModelState.IsValid)
            {
                template.CreatedAt = DateTime.Now;
                _context.Add(template);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Template created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", template.CategoryId);
            return View(template);
        }

        // GET: ECardTemplate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.ECardTemplates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", template.CategoryId);
            return View(template);
        }

        // POST: ECardTemplate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TemplateId,Title,Description,ImageUrl,CreatedAt,CategoryId")] ECardTemplate template)
        {
            if (id != template.TemplateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(template);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Template updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateExists(template.TemplateId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", template.CategoryId);
            return View(template);
        }

        // GET: ECardTemplate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.ECardTemplates
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TemplateId == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // POST: ECardTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var template = await _context.ECardTemplates.FindAsync(id);
            if (template != null)
            {
                _context.ECardTemplates.Remove(template);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Template deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TemplateExists(int id)
        {
            return _context.ECardTemplates.Any(e => e.TemplateId == id);
        }
    }
}
