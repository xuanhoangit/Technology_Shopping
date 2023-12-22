using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.ModelAdmin;
using Shop.ModelProduct;
using Shop.Models;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories.Implements;
[Area("Customer")]
public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext db;

    public ProductRepository(ApplicationDbContext db)
    {
        this.db = db;
    }
    public async Task<DisplayProductByCategory> DisplayProductAndCategory(string search="",int request=0){
            var data=new DisplayProductByCategory();
            var ListProduct=new List<Product>();
            if(request==1){
                ListProduct=await Product_New_DataAsync(search);
                data.CategoryName="Sản phẩm mới";
            }
            if(request==2){
                ListProduct=await BestSeller(search);
                data.CategoryName="Sản phẩm bán chạy";
            }
            if(request==3){
                ListProduct=await Product_DataAsync(search);
                data.CategoryName="Sản phẩm ưu đãi";
            }
            data.ListCategory=await Category_DataAsync();
            foreach(var product in ListProduct){
            product.Image=db.ProductMedias.Where(x=>x.ProductId==product.Id).FirstOrDefault().Image;
        }
            data.Product=ListProduct;
            return data;
    }
    public DisplayProductByCategory DisplayProductByCategory(int CategoryId,int BrandId,string search=""){
        if(BrandId==0){
           CategoryBrand? defaultBrand= db.CategoryBrands.Where(x=>x.CategoryId==CategoryId).FirstOrDefault();
            BrandId=defaultBrand.BrandId;
        }
        var ListProduct=db.Products.Where(x=>x.CategoryId==CategoryId&&x.BrandId==BrandId &&x.Name.ToLower().Contains(search.ToLower())).ToList();
        //var listBrandByCategory
        var listBrandByCategory=from cateBrand in db.CategoryBrands join
                                    category in db.Categories on cateBrand.CategoryId equals category.Id join
                                    brand in db.Brands on cateBrand.BrandId equals brand.Id
                                    where cateBrand.CategoryId == CategoryId
                                    select new CategoryBrand{
                                        CategoryId=category.Id,
                                        BrandId=brand.Id,
                                        CategoryName=category.Name,
                                        BrandName=brand.Name
                                    };
        var ListBrand=listBrandByCategory.ToList();
        foreach(var product in ListProduct){
            product.Image=db.ProductMedias.Where(x=>x.ProductId==product.Id).FirstOrDefault().Image;
        }
        var PageData=new DisplayProductByCategory{
            Product=ListProduct,
            CategoryName=db.Categories.Where(x=>x.Id==CategoryId).FirstOrDefault().Name,
            BrandName=db.Brands.Where(x=>x.Id==BrandId).FirstOrDefault().Name,
            CategoryId=CategoryId,
            BrandId=BrandId,
            ListBrand=ListBrand
        };
        return PageData;
    }
    public async Task<List<Product>> BestSeller(string search=""){
        var orderSuccess=await db.Orders.Where(x=>x.Status==3).ToListAsync();
        var listOrderDetail=new List<OrderDetail>();
        foreach (var order in orderSuccess)
        {
            var orderDetail=await db.OrderDetails.Where(x=>x.OrderId==order.Id).ToListAsync();
            for(int i=0;i<orderDetail.Count();i++){
                bool exist=false;
                for(int j=0;j<listOrderDetail.Count();j++){
                    if(listOrderDetail[j].ProductId==orderDetail[i].ProductId){
                        listOrderDetail[j].Quantity+=orderDetail[i].Quantity;
                        exist=true;
                        break;
                    }
                }
                if(exist==false){
                    listOrderDetail.Add(orderDetail[i]);
                }
            }
        }
        listOrderDetail=listOrderDetail.OrderByDescending(x=>x.Quantity).ToList();
        List<Product> ListProduct=new List<Product>();
        foreach(var item in listOrderDetail){
                var product=db.Products.Where(x=>x.Id==item.ProductId&&x.Name.ToLower().Contains(search.ToLower())).FirstOrDefault();
                product.Image=db.ProductMedias.Where(x=>x.ProductId==item.Id).FirstOrDefault().Image;
                ListProduct.Add(product);
        }
        return ListProduct;
    }
    public async Task<List<Category>> GetBrandByCateGory(){
        var ListCategory=await db.Categories.Where(x=>x.Status==1).ToListAsync();

        foreach (var category in ListCategory)
        {
            var ListBrandCategory=db.CategoryBrands.Where(x=>x.CategoryId==category.Id).ToList();
           if(ListBrandCategory is not null){
            var listBrand=new List<Brand>();
             foreach (var brandcate in ListBrandCategory)
                {
                    var brand = db.Brands.Where(x=>x.Id==brandcate.BrandId).FirstOrDefault();
                    listBrand.Add(brand);
                }
                category.ListBrand=listBrand;
           }
        }
        return ListCategory;
    }
    //DisplayHome
    //Customer
    public async Task<DisplayHomeCustomer> D_HomeAsync()
    {
        var home=new DisplayHomeCustomer{
            FifteenProduct=await Product_DataAsync(),
            NewArrival=await Product_New_DataAsync(),
            Categories=await GetBrandByCateGory(),
            BestSeller=await BestSeller()
        };
        return home;
    }
    public async Task<List<Category>> Category_DataAsync(){
        var data=await db.Categories.Where(x=>x.Status==1).ToListAsync();
        
        return data;
    } 
    public async Task<List<Product>> Product_New_DataAsync(string search=""){
        var data=await db.Products.Where(x=>x.Status==1&&x.Name.ToLower().Contains(search.ToLower())).OrderByDescending(p => p.Id).ToListAsync();
        return data;
    } 
    public async Task<List<Product>> Product_DataAsync(string search=""){
        var data=await db.Products.Where(x=>x.Status==1&&x.Price>x.Sale&&x.Name.ToLower().Contains(search.ToLower())).ToListAsync();
        foreach (var item in data)
        {
            item.Image=db.ProductMedias.Where(x=>x.ProductId==item.Id).FirstOrDefault().Image;
        }
        return data;
    }
    //End DisplayHome
    public async Task<List<Brand>> Display_Brand(){
        return await db.Brands.ToListAsync();
    }
    //Product Detail
    public async Task<DisplayProductDetail> Detail(int id)
    {
        if(id==0){
            throw new Exception("Sản phẩm không tồn tại");
        }   
            var product=db.Products.Find(id);
            DisplayProductDetail? detail = new DisplayProductDetail{
                Product=product,
                Category=db.Categories.Where(x=>x.Id==product.Id).FirstOrDefault(),
                ProductMedias=db.ProductMedias.Where(x=>x.ProductId==product.Id).ToList(),
                ProductDecriptions=db.ProductDecriptions.Where(x=>x.ProductId==product.Id).ToList()
            };
        
            return detail;
        }
    //Admin Section
    public bool Add(Product product, List<string> ProductDecriptions)
    {   List<string> ListNewName=new List<string>();
        using (var transaction=db.Database.BeginTransaction())
        {
            try
            {   
                db.Products.Add(product);
                db.SaveChanges();

                foreach (var media in product.ListFiles)
                {   
                    string newName=Libraries.LibraryProject.HandleFileImage(media);
                    ListNewName.Add(newName);
                    var newMedia=new ProductMedia{
                        Image=newName,
                        ProductId=product.Id
                    };
                    db.ProductMedias.Add(newMedia);
                }
                db.SaveChanges();
                if(ProductDecriptions.Count()!=0){
                    foreach (var decription in ProductDecriptions)
                    {
                        var newDecription=new ProductDecription{
                            Decription=decription,
                            ProductId=product.Id
                        };
                        db.ProductDecriptions.Add(newDecription);
                    }
                    db.SaveChanges();
                }
                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
        int countMedia=ListNewName.Count();
        for (int i=0;i<countMedia;i++)
        {
            var filePathCategory=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images/ProductMedia",ListNewName[i]);
                using (var stream=new FileStream(filePathCategory,FileMode.Create))
                {
                    product.ListFiles[i].CopyTo(stream);
                }  
        }
        return true;
    }

    public bool Update(Product product, List<string> ProductDecriptions)
    {
        throw new NotImplementedException();
    }

    public bool Remove(int id)
    {
        throw new NotImplementedException();
    }

    public ProductAdd GetAProduct(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DisplayProductDetail>> DisplayListProduct()
    {   
        var ListWholeProduct=new  List<DisplayProductDetail>();
        var product=db.Products.ToList();
        foreach(var prod in product){
            var wholeProduct=await Detail(prod.Id);
            ListWholeProduct.Add(wholeProduct);
        }
        return ListWholeProduct;
    }

}