using ConsoleToWebApi.Models;

namespace ConsoleToWebApi.Repository
{
    public interface IProductRespository
    {
        int AddProduct(ProductModel model);
        List<ProductModel> GetAllProducts();

        string GetName();
    }
}