using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Repositories.Interfaces;

namespace Shop.Areas.Customer.Controllers;
[Area("Customer")]
[Authorize]
public class OrderController : Controller
{
    private readonly IOrderRepository repo;

    public OrderController(IOrderRepository repo)
    {
        this.repo = repo;
    }
    public IActionResult Index(){
        var listOrder=repo.DisplayOrder();
        return View(listOrder);
    }
    public IActionResult Checkout(List<int> order){
        if(order.Count==0){
            TempData["RequestChooseProduct"]="Vui lòng chọn sản phẩm để thanh toán";
            return RedirectToAction("Index","Cart");
        }
        var checkout=repo.Checkout(order);
        return View(checkout);

    }
    public IActionResult ConfirmCheckOut(List<int> itemCartId,int addressId,Address address){
        var result=repo.ConfirmCheckOut(itemCartId,addressId,address);
        if(result){
            return RedirectToAction("Index");
        }
        TempData["OrderFail"]="Giao dịch thất bại";
        return Ok();
    }
    public IActionResult GetOrderCount(){
        return Content(repo.GetOrderCount().ToString());
    }

}