using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.ModelProduct;
using Shop.Models;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories.Implements;
public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext db;
    private readonly IHttpContextAccessor http;
    private readonly UserManager<Users> userManager;

    public OrderRepository(ApplicationDbContext db,IHttpContextAccessor http,UserManager<Users> userManager)
    {
        this.db = db;
        this.http = http;
        this.userManager = userManager;
    }
    public DisplayCheckout Checkout(List<int> order){
            var displayCheckout=new DisplayCheckout();
            var listCart=new List<Cart>();
            string userId=GetUserId();
            foreach(var unitP in order)
            {
                var item=db.Carts.Where(x=>x.Id==unitP&& x.UserId==userId).FirstOrDefault();
                listCart.Add(item);
            }
            displayCheckout.ListCart=listCart;
            displayCheckout.ListAddress=db.Addresses.Where(x=>x.UserId==userId).ToList();
            return displayCheckout;
    }
    public bool ConfirmCheckOut(List<int> itemCartId,int addressId,Address address){
       using (var transaction= db.Database.BeginTransaction()){
        try
        {    
            string userId=GetUserId();
            if(addressId==0){
                address.UserId=userId;
                db.Addresses.Add(address);
                db.SaveChanges();
                addressId=address.Id;
            }

            float totalPrice=0;
            foreach (var id in itemCartId)
                {
                    var item=db.Carts.Where(x=>x.Id==id).FirstOrDefault();
                    totalPrice+=item.Quantity*item.Price;
                }
            var order=new Order{
                UserId=userId,
                Total_Price=totalPrice,
                AddressId=addressId
                };
                db.Orders.Add(order);
                db.SaveChanges();
             foreach (var id in itemCartId)
                {
                     var item=db.Carts.Where(x=>x.Id==id).FirstOrDefault();
                     var orderDetail=new OrderDetail{
                        ProductId=item.ProductId,
                        ProductName=item.ProductName,
                        Price=item.Price,
                        UserId=userId,
                        UnitPrice=item.Price*item.Quantity,
                        Quantity=item.Quantity,
                        OrderId=order.Id,
                        Status=1
                     };
                     db.OrderDetails.Add(orderDetail); 
                }
                db.SaveChanges();
                transaction.Commit();
        }
        catch (System.Exception)
        {
            transaction.Rollback();
            return false;
        }
       }
       foreach (var id in itemCartId)
       {
            var item=db.Carts.Where(x=>x.Id==id).FirstOrDefault();
            item.Status=2;
            db.Carts.Update(item);
       }
       db.SaveChanges();
       return true;
    }

    public string GetUserId(){
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var pricipal= http.HttpContext.User;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        string userId=userManager.GetUserId(pricipal);
        return userId;
    }
    public List<DisplayOrder> DisplayOrder(){
        var displayListOrder=new List<DisplayOrder>();
        var listOrder=db.Orders.Where(x=>x.UserId==GetUserId()&& (x.Status==1 ||x.Status==2)).ToList();
        foreach (var order in listOrder)
        {   

            var _listOrderDetail=from odetail in db.OrderDetails join prod in db.Products on odetail.ProductId equals prod.Id
                where odetail.OrderId==order.Id
                 select new OrderDetail {
                    ProductName=prod.Name,
                    UnitPrice=odetail.UnitPrice,
                    Quantity=odetail.Quantity
                };

           var d_order= new DisplayOrder{
                Order=order,
                Address=db.Addresses.Where(x=>x.Id==order.AddressId).FirstOrDefault(),
                ListOrderDetail=_listOrderDetail.ToList()
            } ;
            displayListOrder.Add(d_order);
        }
        return displayListOrder;
    }
    public int GetOrderCount(){
        return db.Orders.Where(x=>x.UserId==GetUserId()&&x.Status==1).ToList().Count();
    }
    public List<DisplayOrder> GetOrder(int status){
                var displayListOrder=new List<DisplayOrder>();
        var listOrder=db.Orders.Where(x=> x.Status==status).ToList();
        foreach (var order in listOrder)
        {   

            var _listOrderDetail=from odetail in db.OrderDetails join prod in db.Products on odetail.ProductId equals prod.Id
                where odetail.OrderId==order.Id
                 select new OrderDetail {
                    ProductName=prod.Name,
                    UnitPrice=odetail.UnitPrice,
                    Quantity=odetail.Quantity,
                    Kho=prod.Quantity
                };
            // var addresses=from address in db.Addresses join user in db.Users on address.UserId equals user.Id
            // where address.Id == order.AddressId && address.UserId==order.UserId
            // select new Address{
            //     WardCode=address.WardCode,
            //     WardName=address.WardName,
            //     DistrictCode=address.DistrictCode,
            //     DistrictName=address.DistrictName,
            //     ProvinceCode=address.ProvinceCode,
            //     ProvinceName=address.ProvinceName,
            //     PhoneNumber=address.PhoneNumber,
            //     CustomerName=user.Ho+" "+user.Ten
            // };
            var user=db.Users.Where(x=>x.Id==order.UserId).FirstOrDefault();
            var address=db.Addresses.Where(x=>x.Id==order.AddressId).FirstOrDefault();
            address.CustomerName=user.Ho+" "+user.Ten;
            if(address.PhoneNumber==null){
                address.PhoneNumber=user.PhoneNumber;
            }
           var d_order= new DisplayOrder{
                Order=order,
                Address=address,
                ListOrderDetail=_listOrderDetail.ToList()
            } ;
            displayListOrder.Add(d_order);
            // if(d_order.Address.PhoneNumber is null){
            //     d_order.Address.PhoneNumber=db.Users.Where(x=>x.Id==d_order.Address.UserId).FirstOrDefault().PhoneNumber;
            // }
        }
        return displayListOrder;
    }

    public bool ChangeStatusOrder(int orderId,int status){
        try
        {   
            var updateStatusToConfirm= db.Orders.Where(x=>x.Id==orderId).FirstOrDefault();
            if(updateStatusToConfirm is not null){
                updateStatusToConfirm.Status=status;
                db.Update(updateStatusToConfirm);
                db.SaveChanges();
            }
        }
        catch (System.Exception)
        {
            return false;
        }
       return true;
    }
   public bool ConfirmOrderSuccess(int orderId){
        try
        {
            var listItemSuccess=db.OrderDetails.Where(x=>x.OrderId==orderId).ToList();
            foreach (var item in listItemSuccess)
            {
                var product=db.Products.Where(x=>x.Id==item.ProductId).FirstOrDefault();
                product.Quantity-=item.Quantity;
                db.Products.Update(product);
            }
            db.SaveChanges();
        }
        catch (System.Exception)
        {
            return false;
        }
        ChangeStatusOrder(orderId,3);
        return true;
   }
}