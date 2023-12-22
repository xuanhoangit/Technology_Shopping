using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Constants;
namespace Shop.Areas.Admin.Controllers;
[Authorize]
[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index(){
        return View();
    }
}