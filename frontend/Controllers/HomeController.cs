using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontend.Models;
using frontend.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public HomeController(
            ILogger<HomeController> logger,
            IWebHostEnvironment env,
            ApplicationDbContext context,
            IConfiguration config)
        {
            _logger = logger;
            _env = env;
            _context = context;
            _config = config;
        }

        public IActionResult Index(int? categoryId, string searchTerm)
        {
            var query = _context.ECardTemplates.Include(e => e.Category).AsQueryable();

            // Apply category filter if specified
            if (categoryId.HasValue)
            {
                query = query.Where(e => e.CategoryId == categoryId.Value);
            }

            // Apply search filter if specified
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(e =>
                    e.Title.Contains(searchTerm) ||
                    e.Description.Contains(searchTerm) ||
                    e.Category.Name.Contains(searchTerm));
            }

            var cards = query.ToList();

            // Pass categories for filter dropdown using ViewBag
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SearchTerm = searchTerm;

            return View(cards);
        }
        public IActionResult AccessDenied() => View();

        public IActionResult Privacy() => View();

        public IActionResult Detail(int id, int? categoryId, string searchTerm)
        {
            var card = _context.ECardTemplates
                .Include(e => e.Category)
                .FirstOrDefault(e => e.TemplateId == id);

            if (card == null)
            {
                return NotFound();
            }

            // Pass filter parameters to view using ViewBag instead of a view model
            ViewBag.CategoryId = categoryId;
            ViewBag.SearchTerm = searchTerm;

            // Return the ECardTemplate directly
            return View(card);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(
     int TemplateId,
     string SenderName,
     string Subject,
     string RecipientEmail,
     string PersonalMessage,
     int? categoryId,
     string searchTerm)
        {
            // ✅ Check if user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to login and preserve return URL to come back after login
                var returnUrl = Url.Action("Detail", new
                {
                    id = TemplateId,
                    categoryId = categoryId,
                    searchTerm = searchTerm
                });

                return RedirectToAction("Login", "Account", new { returnUrl });
            }

            // Fetch E-Card from DB
            var card = _context.ECardTemplates.FirstOrDefault(e => e.TemplateId == TemplateId);
            if (card == null)
            {
                return NotFound();
            }

            try
            {
                var emailSettings = _config.GetSection("EmailSettings");

                var fromAddress = new MailAddress(emailSettings["FromEmail"], emailSettings["FromName"]);
                var toAddress = new MailAddress(RecipientEmail);

                string body = $@"
            <h2>{Subject}</h2>
            <p>From: {SenderName}</p>
            <p>{PersonalMessage}</p>
            <p>You've received an e-card!</p>
            <img src='{Url.Content($"~/uploads/{card.ImageUrl}")}' alt='{card.Title}' style='max-width: 100%;' />";

                using var smtp = new SmtpClient
                {
                    Host = emailSettings["SmtpHost"],
                    Port = int.Parse(emailSettings["SmtpPort"]),
                    EnableSsl = bool.Parse(emailSettings["EnableSsl"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(
                        emailSettings["Username"],
                        emailSettings["Password"]),
                    Timeout = int.Parse(emailSettings["Timeout"])
                };

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = Subject,
                    Body = body,
                    IsBodyHtml = true
                };

                await smtp.SendMailAsync(message);
                TempData["SuccessMessage"] = "E-Card sent successfully!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email");
                TempData["ErrorMessage"] = "Failed to send email. Please try again later.";
            }

            return RedirectToAction("Detail", new
            {
                id = TemplateId,
                categoryId = categoryId,
                searchTerm = searchTerm
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
