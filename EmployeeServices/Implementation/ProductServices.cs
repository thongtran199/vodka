using VodkaEntities;
using VodkaDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            return (IEnumerable<Product>)_context.Products
                .Where(x => x.IsActive.Trim().Equals("1"))
                .ToList();
        }

        public Product? GetById(string id)
        {
            return _context.Products.Where(x => x.ProductNum.Equals(id) && x.IsActive.Trim().Equals("1"))
                                    .Include(x => x.Transactdetail)
                                    .FirstOrDefault();
        }

        public int GetLastId()
        {
            var p =  _context.Products.OrderByDescending(i => i.ProductNum).FirstOrDefault();
            return int.Parse(p.ProductNum.Replace("P",""));
        }

        public async Task UpdateAsSync(Product product)
        {
            _context.Products.Update(product);
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
        public IEnumerable<Product> GetProductsByCategoryId(string CatId)
        {
            return _context.Products.Where(p => p.CatId.Equals(CatId)).ToList();
        }
    }
}
