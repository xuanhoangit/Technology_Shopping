using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace Shop.Models;
public class Users:IdentityUser
{   

    [Required(ErrorMessage ="{0} không được để trống")]
    [Display(Name="Họ")]
    //[AtLeastOneLetter(ErrorMessage ="Chứa ký tự không hợp lệ")]
    [MaxLength(30,ErrorMessage ="Họ không được dài quá 30 ký tự")]
    public string? Ho { get; set; }
    [Required(ErrorMessage ="{0} không được để trống")]
    [Display(Name="Tên")]
    //[AtLeastOneLetter(ErrorMessage ="Chứa ký tự không hợp lệ")]
    [MaxLength(30,ErrorMessage ="Tên không được dài quá 30 ký tự")]
    public string? Ten { get; set; }
    [Required(ErrorMessage ="{0} không được để trống")]
    [Display(Name="Giới tính")]
    public string? GioiTinh { get; set; }
    [Required(ErrorMessage ="{0} không được để trống")]
    [Display(Name="Ngày sinh")]
    //[AgeValidation(12)]
    [DataType(DataType.Date)]
    public DateTime? NgaySinh { get; set; }
    public int Status { get; set; }=1;
}