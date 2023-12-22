using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data;

public class ApplicationDbContext : IdentityDbContext<Users>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductMedia> ProductMedias { get; set; }
    public DbSet<ProductDecription> ProductDecriptions  { get; set; }
    public DbSet<Category> Categories  { get; set; }
    public DbSet<Cart> Carts  { get; set; }
    public DbSet<OrderDetail> OrderDetails  { get; set; }
    public DbSet<Order> Orders  { get; set; }
    public DbSet<Address> Addresses  { get; set; }
    public DbSet<Brand> Brands  { get; set; }
    public DbSet<CategoryBrand> CategoryBrands  { get; set; }

   
}
