using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models;
public class Address
{
    public int Id { get; set; }
    [Required]
    public string? ProvinceName { get; set; }
    [Required]
    public string? ProvinceCode { get; set; }
    [Required]
    public string? DistrictName { get; set; }
    [Required]
    public string? DistrictCode { get; set; }
    [Required]
    public string? WardName { get; set; }
    [Required]
    public string? WardCode { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    public string? Note{ get; set; }
    [Required]
    public string? UserId { get; set; }
    public Users? Users { get; set; }
    public int Status { get; set; }=1;
    [NotMapped]
    public string? CustomerName { get; set; }
}