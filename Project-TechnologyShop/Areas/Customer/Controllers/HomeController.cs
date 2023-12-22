using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Repositories.Interfaces;
namespace Shop.Areas.Customer.Controllers;
[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductRepository repo;

    public HomeController(ILogger<HomeController> logger,IProductRepository repo)
    {
        _logger = logger;
        this.repo = repo;
    }

    public async Task<IActionResult> Index()
    {   
        var home=await repo.D_HomeAsync();
        return View(home);
    }
    [HttpGet]
    public async Task<IActionResult> IndexData()
     {   
        var catebrand=await repo.GetBrandByCateGory();
        return Json(catebrand);
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
