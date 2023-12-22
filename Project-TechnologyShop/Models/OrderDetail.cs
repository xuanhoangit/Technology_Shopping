using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models;
public class OrderDetail
{
    public int Id { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public float UnitPrice { get; set; }
    [Required]
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    [Required]
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    [Required]
    public string? UserId { get; set; }
    public Users? User { get; set; }
    [NotMapped]
    public string? ProductName { get; set; }
    [NotMapped]
    public float Price { get; set; }
    [Required]
    public int Status { get; set; }=1;
    [NotMapped]
    public int Kho { get; set; }
}