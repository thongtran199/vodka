using VodkaEntities;
using VodkaDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;

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
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }


        public IEnumerable<Product> GetAll()
        {
            return _context.Products
                .ToList();
        }

        public Product? GetById(string id)
        {
            return _context.Products.Where(x => x.ProductId.Equals(id))
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

            return _context.Products
                .Where(p => p.Price <= maxPrice && p.Price >= minPrice)
                .OrderBy(p => p.Price)
                .ToList();

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

        public int TotalFilterProductByName(string str)
        {
            return FilterProductByName(str).Count();
        }

        public int TotalProductFilterByPrice(decimal minPrice, decimal maxPrice)
        {
            return FilterProductByPrice(minPrice, maxPrice).Count();
        }

        public int TotalProductByCategoryId(string id)
        {
            return GetProductsByCategoryId(id).Count();
        }

        public int TotalProduct()
        {
            return GetAll().Count();
        }

        public IEnumerable<Product> GetProductByPage(int page)
        {
            var start = page == 1 ? 1 : (page - 1) * 13;
            var end = start + 12;
            return GetProductFromMToN(start, end);
        }

        public async Task<IdentityResult> UpdateQuantityAsync(string productId, int quantity)
        {
            var product = GetById(productId);
            if(product == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Khong tim thay Product" });

            }
            product.Quan = quantity;
            await UpdateAsSync(product);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateIsActiveAsync(string productId, int isActive)
        {
            var product = GetById(productId);
            if (product == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Khong tim thay Product" });
            }
            product.IsActive = isActive;
            await UpdateAsSync(product);
            return IdentityResult.Success;
        }
    }
}
