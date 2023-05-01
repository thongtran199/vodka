using VodkaEntities;
using VodkaDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

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

        public IEnumerable<Product> FilterProductByPrice(float minPrice, float maxPrice)
        {
            var products = GetAll();
            var filterProducts = new List<Product>();

            if ( maxPrice == 0)
            {
                foreach (var p in products)
                {
                    if (float.Parse(p.Price) >= minPrice)
                    {
                        filterProducts.Add(p);
                    }
                }
            }

            else
            {
                foreach (var p in products)
                {
                    if (float.Parse(p.Price) >= minPrice && float.Parse(p.Price) <= maxPrice)
                    {
                        filterProducts.Add(p);
                    }
                }
            }
            return filterProducts;
        }

        public Product GetProductByName(string name) => _context.Products.Where(x => x.ProductName.ToUpper().Equals(name.ToUpper(), StringComparison.Ordinal) && x.IsActive.Trim().Equals("1", StringComparison.Ordinal)).FirstOrDefault();

        public IEnumerable<Product> FilterProductByName(string str)
        {
            return _context.Products.Where(x => x.IsActive.Trim().Equals("1") && x.ProductName.ToUpper().Contains(str.ToUpper())).ToList();
        }

        public IEnumerable<Product> FilterProductPriceIncrease()
        {
           var products = GetAll();
            var sortedProducts = products.OrderBy(p => p.Price);

            return sortedProducts;
        }
        public IEnumerable<Product> FilterProductPriceDecrease()
        { var products = GetAll();
            var sortedProducts = products.OrderByDescending(p => p.Price);

            return sortedProducts;
            
        }
        public IEnumerable<Product> GetProductFromMToN(int m, int n)
        {
            return _context.Products.OrderBy(p => p.ProductNum)
                .Skip(m - 1)
                .Take(n - m + 1)
                .ToList();
        }

       
    }
}
