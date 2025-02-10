using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public IActionResult Index()
    {
        _logger.LogInformation($"Called {nameof(HomeController)}.{nameof(this.Index)}()");
        return View();
    }

    public IActionResult Privacy()
    {
        _logger.LogInformation($"Called {nameof(HomeController)}.{nameof(this.Privacy)}()");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError($"Error happened, called {nameof(HomeController)}.{nameof(this.Error)}()");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}