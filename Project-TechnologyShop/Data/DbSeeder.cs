
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Shop.Models;
using Shop.Constants;

namespace BookShopping.Web.Data;
public class DbSeeder 
{   

    
    public static async Task SeedDefaultData(IServiceProvider service){
        var userManager = service.GetService<UserManager<Users>>();
        var roleManager = service.GetService<RoleManager<IdentityRole>>();

        //Add role to db
        await roleManager.CreateAsync(new IdentityRole(Roles.Customer));
        await roleManager.CreateAsync(new IdentityRole(Roles.Staff.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

        //create admin account
        var admin=new Users{
            UserName="admincodengu@gmail.com",
            Email="admincodengu@gmail.com",
            Ho="Jame",
            Ten="Tran",
            GioiTinh="Male",
            NgaySinh=DateTime.Parse("2001-08-22"),
            PhoneNumber="0368154633"
    
        };
        var isExistAdmin=await userManager.FindByEmailAsync(admin.Email);
        if(isExistAdmin is null){
            await userManager.CreateAsync(admin,"Txhoang11!");
            await userManager.AddToRoleAsync(admin,Roles.Admin.ToString());
        }
        
    }
}