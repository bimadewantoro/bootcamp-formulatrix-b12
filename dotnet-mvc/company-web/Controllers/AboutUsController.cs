using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using company_web.Models;

namespace company_web.Controllers;

public class AboutUsController : Controller
{
    private readonly ILogger<AboutUsController> _logger;

    public AboutUsController(ILogger<AboutUsController> logger)
    {
        _logger = logger;
    }

    [Route("about-us", Name = "AboutUs")]
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
