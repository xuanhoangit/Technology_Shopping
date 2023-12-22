// function ClickZoom(image,id){
//     var containerImage=document.querySelector("#showImage");
//     containerImage.setAttribute("class","position-fixed  w-100 d-flex justify-content-between");
//     containerImage.style.zIndex="100";
//     containerImage.style.backgroundColor="rgba(220,53,69,0.5)";

//     var containerImgMain=document.createElement("div");
//     containerImgMain.setAttribute("class","w-100")
//     containerImage.appendChild(containerImgMain);

//     var imgMain=document.createElement("img");
//     imgMain.src=image.src;
//     imgMain.setAttribute("class","mx-auto")
//     imgMain.style.width="800px";
//     imgMain.style.height="400px";
//     containerImgMain.appendChild(imgMain)

//     var containerActive=document.createElement("div");
//     containerActive.style.width="100px";
//     containerActive.setAttribute("class","position-absolute")
//     containerImage.appendChild(containerActive);
//         var listImg=document.querySelectorAll("#"+id+" img");
//         listImg.forEach(function (x){
//             var img=document.createElement("img");
//             img.src=x.src;
//             img.setAttribute("class","my-2")
//             img.style.width="100%";
//             containerActive.appendChild(img)
//         })
//         console.log(listImg);
//   }
  
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