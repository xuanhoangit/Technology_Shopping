using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.ModelAdmin;
using Shop.Models;
using Shop.Repositories.Interfaces;

namespace Shop.Areas.Admin.Controllers;
[Authorize]
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductRepository repo;

    public ProductController(IProductRepository repo)
    {
        this.repo = repo;
    }
    public async Task<IActionResult> Index(){
        
        return View(await repo.DisplayListProduct());
    }
    public async Task<IActionResult> Add(){
         var category=new ProductAdd{
            ListCategory=await repo.Category_DataAsync(),
            ListBrand=await repo.Display_Brand(),
            Product=new Product(),
            ProductDecription=new ProductDecription(),
            ProductMedia=new ProductMedia()
        };
        return View(category);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Product product,List<string> ProductDecriptions){
       if(ModelState.IsValid){
            var result=repo.Add(product,ProductDecriptions);
            if(result){
                return RedirectToAction("Index");
            }
       }
        var category=new ProductAdd{
            ListCategory=await repo.Category_DataAsync(),
            Product=new Product(),
            ProductDecription=new ProductDecription(),
            ProductMedia=new ProductMedia()
        };
        return View(category);
    }
    public IActionResult Update(){
        return View();
    }
    public IActionResult Remove(){
        //var product=db.Products.
        return View();
    }


}