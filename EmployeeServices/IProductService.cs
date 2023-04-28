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
        IEnumerable<Product> GetAll();

        int GetLastId();

        IEnumerable<Product> GetProductsByCategoryId(string id);

        IEnumerable<Product> FilterProductByPrice(float minPrice, float maxPrice);
        //IEnumerable<SelectListItem> GetAllProductsForPayroll();
    }
}
