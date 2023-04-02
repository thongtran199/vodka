using VodkaEntities;
using VodkaDataAccess;

namespace VodkaServices.Implementation
{
    public class ProductServices : IProductService
    {
        private ApplicationDbContext _context;
        public ProductServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsSync(Product newProduct)
        {
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(string id)
        {
            var product = GetById(id);
            product.IsActive = "0";
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }


        public IEnumerable<Product> GetAll()
        {
            return _context.Products.Where(x => x.IsActive.Trim().Equals("1")).ToList();
        }

        public Product GetById(string id)
        {
            return _context.Products.Where(x => x.ProductNum.Equals(id) && x.IsActive.Trim().Equals("1"))
                                    .FirstOrDefault();
        }

        public async Task UpdateAsSync(Product newProduct)
        {
            _context.Products.Update(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateById(string id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
