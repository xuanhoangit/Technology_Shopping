using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Libraries;
public class LibraryProject
{   
    public static string StringTrimming(string Text){
        string[] words = Text.Split(" ");
        int wordsLeng=words.Length;
        string newText="";
        for(int i=0;i<wordsLeng;i++){
           if(words[i]!=""){
            newText=newText.Trim()+" "+words[i];
           }
        }
        return newText;
    }
   
    public static bool PhoneNumberValidation(string PhoneNumber){
        PhoneNumber=PhoneNumber.Trim();
        if(PhoneNumber.Length<10 || PhoneNumber.Length>15){
            return false;
        }
        if(ContainsAlphabetic(PhoneNumber)|| ContainsSpecialCharacter(PhoneNumber)){
            return false;
        }
        return true;
    }
    public static bool ContainsAlphabetic(string input)
    {   
    // Kiểm tra xem chuỗi có chứa bất kỳ ký tự nào không nằm trong bảng chữ cái
    for (int i = 0; i < input.Length; i++)
    {
        // Nếu ký tự tại vị trí i nằm trong bảng chữ cái thì trả về true
        if (Char.IsLetter(input[i]))
        {
            return true;
        }
    }
    // Trả về false
    return false;
}
    public static bool IsOver12(DateTime? birthDate)
{
    // Nếu ngày tháng năm sinh không được xác định thì trả về false
    if (birthDate == null)
    {
        return false;
    }

    // Lấy ngày hiện tại
    DateTime today = DateTime.Now;

    // Tính toán số năm chênh lệch
    int age = today.Year - birthDate.Value.Year;

    // Nếu số năm chênh lệch lớn hơn hoặc bằng 12 thì trả về true
    if (age >= 16)
    {
        return true;
    }
    return false;
}
    public static bool ContainsSpecialCharacter(string input)
        {
        // Lấy danh sách các ký tự đặc biệt và số
        string specialCharactersAndNumbers = "!@#$%^&*()_+-=[]{};:'\",./<>?";

        // Kiểm tra từng ký tự trong chuỗi
        for (int i = 0; i < input.Length; i++)
        {
            // Nếu ký tự là ký tự đặc biệt hoặc số thì trả về true
            if (specialCharactersAndNumbers.Contains(input[i]))
            {
            return true;
            }
        }
        // Nếu không tìm thấy ký tự đặc biệt hoặc số nào thì trả về false
        return false;
    }
    public static bool ContainsNumber(string input)
{
    // Kiểm tra xem chuỗi có chứa bất kỳ ký tự số nào không
    for (int i = 0; i < input.Length; i++)
    {
        // Nếu ký tự là ký tự số thì trả về true
        if (Char.IsDigit(input[i]))
        {
            return true;
        }
    }
    // Nếu không tìm thấy ký tự số nào thì trả về false
    return false;
}   
        public static bool InputValidationForFullName(string fullName){
        fullName=fullName.Trim();
        if(fullName.Length<5 || fullName.Length>50){
            return false;
        }
        if(ContainsSpecialCharacter(fullName)||ContainsNumber(fullName)){
            return false;
        }
        return true;
    }
        public static string RandomString()
    {   
        char[] _characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToArray();

        var random = new Random();
        var builder = new StringBuilder();

        for (int i = 0; i < 20; i++)
        {
            builder.Append(_characters[random.Next(_characters.Length)]);
        }

        return "NameProject-"+DateTime.Now.ToString("dd-MM-yyyy")+"-"+builder.ToString();
    }
     public static string HandleFileImage(IFormFile file){
        var fileName=file.FileName;
        var divideFileName=fileName.Split(".");
        var ext=divideFileName.Last();
        var fullName=RandomString()+"."+ext;

        if(file != null && (file.Length>0 &&file.Length<104857600)){
            
            string[] validExtension={"png","jpeg","jpg","gif","jfif","webp"};
            var checkExtension=validExtension.Any(x=>x == ext);
            if(checkExtension){
                
                return fullName;
            }
        }
        return null;
    }
}