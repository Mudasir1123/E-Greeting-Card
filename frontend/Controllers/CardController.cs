using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontend.Models;
using frontend.Data;
using Microsoft.EntityFrameworkCore;
namespace frontend.Controllers;

public class CardController : Controller
{
    private readonly ILogger<CardController> _logger;

    public CardController(ILogger<CardController> logger, IWebHostEnvironment env, ApplicationDbContext context)
    {
        _logger = logger;
        _env = env;
        _context = context;
    }

    private readonly IWebHostEnvironment _env;
    private readonly ApplicationDbContext _context;



    public IActionResult Index()
    {
        var cards = _context.ECardTemplates.Include(e => e.Category).ToList();
        return View(cards);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
