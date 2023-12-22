using Microsoft.AspNetCore.Mvc;
using Shop.Repositories.Interfaces;

namespace Shop.Areas.Customer.Controllers;
[Area("Customer")]
public class ProductController : Controller
{
    private readonly IProductRepository repo;

    public ProductController(IProductRepository repo)
    {
        this.repo = repo;
    }
    public async Task<IActionResult> Detail(int id){
        var detail=await repo.Detail(id);
        return View(detail);
    }
    public IActionResult DisplayProductByCategory(int CategoryId,int BrandId=0,string search="",int productStatus=0){
        var listProduct=repo.DisplayProductByCategory(CategoryId,BrandId,search);
        return View(listProduct);
    }
    public async Task<IActionResult> DisplayProduct(string search="",int request=0){
        TempData["request"]=request;
        var data=await repo.DisplayProductAndCategory(search,request);
        return View(data);
    }
}