using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Repositories.Interfaces;

namespace Shop.Areas.Customer.Controllers;
[Area("Customer")]
[Authorize]
public class CartController : Controller
{
    private readonly ICartRepository repo;

    public CartController(ICartRepository repo)
    {
        this.repo = repo;
    }
    public async Task<IActionResult> Index(){
        return View(await repo.GetCart());
    }
    public IActionResult AddToCart(int id){
        var addItem=repo.AddToCart(id);
        return Ok();
    }
    public IActionResult DeleteItem(int id){
        repo.RemoveItem(id);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> GetCartCount(){
        var count=await repo.GetCart();
        return Content(count.Count().ToString());
    }
    public IActionResult ChangeQuantity(int id,int n){
        var quantity= repo.ChangeItemQuantity(id,n);
        return Ok();

    }
}