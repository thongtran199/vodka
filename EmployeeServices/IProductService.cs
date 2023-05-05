using VodkaEntities;

namespace VodkaServices
{
    public interface IProductService
    {
        Task CreateAsSync(Product newProduct);
        Task UpdateById(string id);
        Task UpdateAsSync(Product newProduct);
        Task DeleteById(string id);
        Product GetById(string id);
        Product GetProductByName(string name);
        IEnumerable<Product> FilterProductByName(string str);
        IEnumerable<Product> FilterProductByPrice(decimal minPrice, decimal maxPrice);
        IEnumerable<Product> GetProductsByCategoryId(string id);
        IEnumerable<Product> GetProductFromMToN(int m, int n);
        IEnumerable<Product> GetAll();

        int GetLastId();

        

        
        //IEnumerable<SelectListItem> GetAllProductsForPayroll();
    }
}
