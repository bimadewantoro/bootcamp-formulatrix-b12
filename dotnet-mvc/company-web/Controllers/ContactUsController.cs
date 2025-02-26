using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using company_web.Models;

namespace company_web.Controllers;

public class ContactUsController : Controller
{
    private readonly ILogger<ContactUsController> _logger;

    public ContactUsController(ILogger<ContactUsController> logger)
    {
        _logger = logger;
    }

    [Route("contact-us", Name = "ContactUs")]
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
