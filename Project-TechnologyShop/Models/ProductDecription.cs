using System.ComponentModel.DataAnnotations;

namespace Shop.Models;
public class ProductDecription
{
    public int Id { get; set; }
    public string? Decription { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Status { get; set; }=1;
}