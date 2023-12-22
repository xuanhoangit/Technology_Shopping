using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models;
public class Cart
{
    public int Id { get; set; }
    [Required]
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    [Required]
    public string? UserId { get; set; }
    public Users? User { get; set; }
    [Required]
    public string? ProductName { get; set; }
    [Required]
    public float Price { get; set; }
    [Required]
    public int Quantity { get; set; }=1;
    public int Status { get; set; }=1;
}