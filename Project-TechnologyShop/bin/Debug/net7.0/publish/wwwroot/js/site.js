// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

////
//GetOrderCOunt
function GetOrderCount(){
    $.ajax({
      type:"POST",
      url:"/Customer/Order/GetOrderCount",
      success:function(count){
        document.querySelector("#orderCount").innerHTML=count
        callback(count);
      },
    })
}
GetOrderCount()
//Add to cart

function GetCartCount(callback){
    $.ajax({
      type:"POST",
      url:"/Customer/Cart/GetCartCount",
      success:function(count){
        document.querySelector("#cartItemCount").innerHTML=count
        callback(count);
      },
    })
  }
  GetCartCount(function (count){})
// Lấy giá trị trong thẻ h2
// Đổi snag vnđ
document.addEventListener('DOMContentLoaded', function() {
    var priceElement = document.querySelectorAll('.money');
    
    priceElement.forEach(function (x){
        var originalValue = x.innerHTML;
        // Chuyển đổi giá trị thành số
        var numberValue = parseFloat(originalValue);

        // Định dạng số thành chuỗi có dấu phân cách hàng nghìn
        var formattedValue = numberValue.toLocaleString('vi-VN');

        // Gán giá trị mới cho thẻ h2
        x.innerHTML = formattedValue +"đ";
    }) 
});


// Write your JavaScript code.


//Account button
document.addEventListener('DOMContentLoaded', function () {
    var toggleHeader = document.getElementById('showMenuAccount');
    var toggleList = document.getElementById('logout');


    toggleHeader.addEventListener('click', function (event) {
       
        event.stopPropagation();
        toggleList.classList.toggle('hidden');
    });
    
    document.addEventListener('click', function () {
        toggleList.classList.add('hidden');
    });

    toggleList.addEventListener('click', function (event) {
        event.stopPropagation();
    });
});
