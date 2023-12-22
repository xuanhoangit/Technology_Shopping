using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Repositories.Interfaces;

namespace Shop.Areas.Admin.Controllers;
[Authorize]
[Area("Admin")]
public class OrderController : Controller
{
    private readonly IOrderRepository repo;

    public OrderController(IOrderRepository repo)
    {
        this.repo = repo;
    }
    public IActionResult ViewOrderRequiring(){
        
        return View(repo.GetOrder(1));
    }
    public IActionResult ViewOrderConfirmed(){
        
        return View(repo.GetOrder(2));
    }
    public IActionResult ViewOrderCanceled(){
        
        return View(repo.GetOrder(0));
    }
    public IActionResult ViewOrderSuccess(){
        
        return View(repo.GetOrder(3));
    }
    public IActionResult Confirm(int orderId){
        var result=repo.ChangeStatusOrder(orderId,2);
       // if(result)
          //  return RedirectToAction("ViewOrderConfirmed");
        return RedirectToAction("ViewOrderRequiring");
    }
    public IActionResult Success(int orderId){
        var result=repo.ConfirmOrderSuccess(orderId);
       // if(result)
          //  return RedirectToAction("ViewOrderConfirmed");
        return RedirectToAction("ViewOrderConfirmed");
    }
    public IActionResult CancelToRequiring(int orderId){
        var result=repo.ChangeStatusOrder(orderId,1);
       // if(result)
          //  return RedirectToAction("ViewOrderConfirmed");
        return RedirectToAction("ViewOrderConfirmed");
    }
    public IActionResult Cancel(int orderId){
        var result=repo.ChangeStatusOrder(orderId,0);
        //if(result)
          //  return RedirectToAction("ViewOrderCanceled");
        return RedirectToAction("ViewOrderRequiring");
    }
}