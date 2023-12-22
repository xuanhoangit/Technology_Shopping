using System.Data.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using Shop.Repositories.Interfaces;
namespace Shop.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class AddressController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor http;
        private readonly UserManager<Users> userManager;
        private readonly IAddressRepository repo;

        public AddressController(ApplicationDbContext db,IHttpContextAccessor http,UserManager<Users> userManager,IAddressRepository repo)
        {
            this.db = db;
            this.http = http;
            this.userManager = userManager;
            this.repo = repo;
        }
        public async Task<IActionResult> Index(){
            return View(await repo.GetAddresses());
        }
        
        // public IActionResult Update(){
        //     var address=db.Addresses.Where(x=>x.UserId=="Default").ToList();
        //    foreach (var item in address)
        //    {
        //         item.UserId="86337109-8c24-43b7-b4ab-1236cc66808f";
        //         db.Addresses.Update(item);
        //         db.SaveChanges();
        //    }
           
        //    return Ok();
        // }
        public IActionResult Add(){
            return View();
        }
        [HttpPost]
        public IActionResult Add(Address address){
            var principal=http.HttpContext.User;
            address.UserId=userManager.GetUserId(principal);
            if(ModelState.IsValid){
                var result=repo.AddNewAddress(address);
                //if(result)
                //return RedirectToAction("Index");
            }
            return View();
        }
    }
}