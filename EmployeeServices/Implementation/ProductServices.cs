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
            product.IsActive = 0;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }


        public IEnumerable<Product> GetAll()
        {
            return _context.Products
                .Where(x => x.IsActive == 1)
                .ToList();
        }

        public Product? GetById(string id)
        {
            return _context.Products.Where(x => x.ProductId.Equals(id) && x.IsActive == 1)
                                    .Include(x => x.TransactDetails)
                                    .FirstOrDefault();
        }

        public int GetLastId()
        {
            var p =  _context.Products.OrderByDescending(i => i.ProductId).FirstOrDefault();
            return int.Parse(p.ProductId.Replace("P",""));
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
            return _context.Products.Where(p => p.CategoryId.Equals(CatId)).ToList();
        }

        public IEnumerable<Product> FilterProductByPrice(decimal minPrice, decimal maxPrice)
        {
            var products = GetAll();
            var filterProducts = new List<Product>();

            if ( maxPrice == 0)
            {
                foreach (var p in products)
                {
                    if (minPrice.CompareTo(p.Price) <= 0)
                    {
                        filterProducts.Add(p);
                    }
                }
            }

            else
            {
                foreach (var p in products)
                {
                    if (maxPrice.CompareTo(p.Price) >= 0)
                    {
                        filterProducts.Add(p);
                    }
                }
            }
            return filterProducts;



        }

        public Product GetProductByName(string name)
        {
            return _context.Products
                .Where(x => x.Name.ToUpper().Equals(name.ToUpper()) && x.IsActive == 1)
                .FirstOrDefault();
        }

        public IEnumerable<Product> FilterProductByName(string str)
        {
            return _context.Products.Where(x => x.IsActive == 1 && x.Name.ToUpper().Contains(str.ToUpper())).ToList();
        }

        public IEnumerable<Product> GetProductFromMToN(int m, int n)
        {
            return _context.Products.OrderBy(p => p.ProductId)
                .Skip(m - 1)
                .Take(n - m + 1)
                .ToList();
        }
    }
}
