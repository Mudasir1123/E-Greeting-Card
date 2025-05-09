//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using frontend.Data;

//namespace ECardWebsite.Controllers
//{
//    public class TransactionController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public TransactionController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Transaction
//        public async Task<IActionResult> Index()
//        {
//            var transactions = await _context.Transactions
//                .Include(t => t.Template)
//                .ToListAsync();
//            return View(transactions);
//        }

//        // GET: Transaction/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var transaction = await _context.Transactions
//                .Include(t => t.Template)
//                .FirstOrDefaultAsync(m => m.TransactionId == id);

//            if (transaction == null)
//            {
//                return NotFound();
//            }

//            return View(transaction);
//        }

//        // GET: Transaction/Create
//        public IActionResult Create()
//        {
//            ViewData["TemplateId"] = new SelectList(_context.ECardTemplates, "TemplateId", "Title");
//            return View();
//        }

//        // POST: Transaction/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("TemplateId,RecipientEmail,Subject,Message")] Transaction transaction)
//        {
//            if (ModelState.IsValid)
//            {
//                transaction.SentAt = DateTime.Now;
//                _context.Add(transaction);
//                await _context.SaveChangesAsync();
//                TempData["SuccessMessage"] = "Transaction created successfully!";
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["TemplateId"] = new SelectList(_context.ECardTemplates, "TemplateId", "Title", transaction.TemplateId);
//            return View(transaction);
//        }

//        // GET: Transaction/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var transaction = await _context.Transactions.FindAsync(id);
//            if (transaction == null)
//            {
//                return NotFound();
//            }
//            ViewData["TemplateId"] = new SelectList(_context.ECardTemplates, "TemplateId", "Title", transaction.TemplateId);
//            return View(transaction);
//        }

//        // POST: Transaction/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,TemplateId,RecipientEmail,Subject,Message,SentAt")] Transaction transaction)
//        {
//            if (id != transaction.TransactionId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(transaction);
//                    await _context.SaveChangesAsync();
//                    TempData["SuccessMessage"] = "Transaction updated successfully!";
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!TransactionExists(transaction.TransactionId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["TemplateId"] = new SelectList(_context.ECardTemplates, "TemplateId", "Title", transaction.TemplateId);
//            return View(transaction);
//        }

//        // GET: Transaction/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var transaction = await _context.Transactions
//                .Include(t => t.Template)
//                .FirstOrDefaultAsync(m => m.TransactionId == id);
//            if (transaction == null)
//            {
//                return NotFound();
//            }

//            return View(transaction);
//        }

//        // POST: Transaction/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var transaction = await _context.Transactions.FindAsync(id);
//            if (transaction != null)
//            {
//                _context.Transactions.Remove(transaction);
//                await _context.SaveChangesAsync();
//                TempData["SuccessMessage"] = "Transaction deleted successfully!";
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        private bool TransactionExists(int id)
//        {
//            return _context.Transactions.Any(e => e.TransactionId == id);
//        }
//    }
//}

