using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories.Implements;
public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext db;
    private readonly IHttpContextAccessor http;
    private readonly UserManager<Users> userManager;

    public CartRepository(ApplicationDbContext db,IHttpContextAccessor http,UserManager<Users> userManager)
    {
        this.db = db;
        this.http = http;
        this.userManager = userManager;
    }
    public bool AddToCart(int id)
    {   

        using (var transaction= db.Database.BeginTransaction())
        {
            try
            {   if(id!=0){
                var getItem=db.Carts.Where(x=>x.ProductId==id&&x.UserId==GetUserId()&&x.Status==1).FirstOrDefault();
                if(getItem is null) {
                var product=db.Products.Where(x=>x.Id==id).FirstOrDefault();
                var item=new Cart{
                    ProductId=id,
                    UserId=GetUserId(),
                    ProductName=product.Name,
                    Price=product.Sale
                };
                db.Carts.Add(item);
                db.SaveChanges();
                transaction.Commit();
                    }
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
        return true;
    }
    public string GetUserId(){
        var pricipal= http.HttpContext.User;
        string userId=userManager.GetUserId(pricipal);
        return userId;
    }
    public async Task<List<Cart>> GetCart()
    {   
        var cart=db.Carts.Where(x=>x.UserId==GetUserId()&& x.Status==1);
        var listCart=await cart.ToListAsync();
        foreach (var item in listCart)
        {
            ChangeItemQuantity(item.Id,item.Quantity);
        }
        return listCart;
    }
    public bool ChangeItemQuantity(int id,int n){
        var item=db.Carts.Where(x=>x.Id==id&& x.Status==1).FirstOrDefault();
        
        var kho=db.Products.Where(x=>x.Id==item.ProductId).FirstOrDefault().Quantity;
        if(n>=kho){
            n=kho;
        }
        item.Quantity=n;
        db.Update(item);
        db.SaveChanges();
        return true;
    }
    public bool RemoveItem(int id)
    {   
        var itemDelete=db.Carts.Where(x=>x.Id==id&& x.Status==1).FirstOrDefault();
        itemDelete.Status=0;
        db.Update(itemDelete);
        db.SaveChanges();
        return true;
    }
}