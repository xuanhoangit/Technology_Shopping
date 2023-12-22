using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories.Implements;
public class AddressRepository : IAddressRepository
{   
    private readonly ApplicationDbContext db;
    private readonly IHttpContextAccessor http;
    private readonly UserManager<Users> userManager;

    public AddressRepository(ApplicationDbContext db,IHttpContextAccessor http,UserManager<Users> userManager)
    {
        this.db = db;
        this.http = http;
        this.userManager = userManager;
    }
    public bool AddNewAddress(Address address)
    {   
       db.Addresses.Add(address);
       db.SaveChanges();
       return true;
    }

    public async Task<List<Address>> GetAddresses()
    {   
        string userId=GetUserId();
        var address=await db.Addresses.ToListAsync();
        return address;
    }

    public bool RemoveAddress(int id)
    {
        throw new NotImplementedException();
    }
    public string GetUserId(){
        var principal=http.HttpContext.User;
        return userManager.GetUserId(principal);
    }
    

}
