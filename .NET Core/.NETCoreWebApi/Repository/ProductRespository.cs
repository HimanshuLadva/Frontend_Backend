using ConsoleToWebApi.Models;

namespace ConsoleToWebApi.Repository
{
    public class ProductRespository : IProductRespository
    {
        private List<ProductModel> products = new List<ProductModel>();
        public int AddProduct(ProductModel model)
        {
            model.Id = products.Count + 1;
            products.Add(model);
            return model.Id;
        }

        public List<ProductModel> GetAllProducts()
        {
            return products;
        }

        public string GetName()
        {
            return "Name from ProductRespository";
        }
    }
}
