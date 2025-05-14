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

        public HomeController(ILogger<HomeController> logger,
                    IWebHostEnvironment env,
                    ApplicationDbContext context,
                    IConfiguration config)
        {
            _logger = logger;
            _env = env;
            _context = context;
            _config = config;
        }

        public IActionResult Index()
        {
            var cards = _context.ECardTemplates.Include(e => e.Category).ToList();
            return View(cards);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ✅ Add Detail Action
        public IActionResult Detail(int id)
        {
            var card = _context.ECardTemplates
                .Include(e => e.Category)
                .FirstOrDefault(e => e.TemplateId == id);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // ✅ Add SendEmail Action (POST)
        [HttpPost]
        public async Task<IActionResult> SendEmail(int TemplateId, string SenderName, string Subject, string RecipientEmail, string PersonalMessage)
        {
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

            return RedirectToAction("Detail", new { id = TemplateId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
