using Shop.ModelAdmin;
using Shop.ModelProduct;
using Shop.Models;

namespace Shop.Repositories.Interfaces;
public interface IProductRepository
{
    Task<DisplayHomeCustomer> D_HomeAsync();
    Task<DisplayProductDetail> Detail(int id);
    Task<List<Category>> Category_DataAsync();
    Task<List<Brand>> Display_Brand();
    Task<List<Category>> GetBrandByCateGory();
    bool Add(Product product,List<string> ProductDecriptions);
    bool Update(Product product,List<string> ProductDecriptions);
    bool Remove(int id);
    Task<List<DisplayProductDetail>> DisplayListProduct();
    DisplayProductByCategory DisplayProductByCategory(int CategoryId,int BrandId,string search="");
    Task<DisplayProductByCategory> DisplayProductAndCategory(string search="",int request=0);
}