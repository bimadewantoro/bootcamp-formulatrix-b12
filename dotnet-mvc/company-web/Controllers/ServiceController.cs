using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using company_web.Models;

namespace company_web.Controllers;

public class ServiceController : Controller
{
    private readonly ILogger<ServiceController> _logger;

    public ServiceController(ILogger<ServiceController> logger)
    {
        _logger = logger;
    }

    [Route("services", Name = "Services")]
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
