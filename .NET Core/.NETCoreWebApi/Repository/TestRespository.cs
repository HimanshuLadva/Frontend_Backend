using ConsoleToWebApi.Models;

namespace ConsoleToWebApi.Repository
{
    public class TestRespository : IProductRespository
    {
        public int AddProduct(ProductModel model)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return "Name from TestRespository";
        }
    }
}
