using System.ComponentModel.DataAnnotations;

namespace Shop.Models;
public class Order
{
    public int Id { get; set; }
    [Required]
    public int AddressId { get; set; }
    public Address? Address { get; set; }
    [Required]
    public float Total_Price { get; set; }
    public DateTime CreateAt { get; set; }=DateTime.Now;
    [Required]
    public string? UserId { get; set; }
    public Users? Users { get; set; }
    public int Status { get; set; }=1;
}