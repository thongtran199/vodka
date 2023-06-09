﻿using Microsoft.AspNetCore.Identity;
using VodkaEntities;

namespace VodkaServices
{
    public interface IProductService
    {
        Task<IdentityResult> UpdateIsActiveAsync(string productId, int isActive);
        Task<IdentityResult> UpdateQuantityAsync(string productId, int quantity);
        Task CreateAsSync(Product newProduct);
        Task UpdateById(string id);
        Task UpdateAsSync(Product newProduct);
        Task DeleteById(string id);
        Product GetById(string id);
        Product GetProductByName(string name);
        IEnumerable<Product> FilterProductByName(string str);
        int TotalFilterProductByName(string str);
        IEnumerable<Product> FilterProductByPrice(decimal minPrice, decimal maxPrice);
        int TotalProductFilterByPrice(decimal minPrice, decimal maxPrice);
        IEnumerable<Product> GetProductsByCategoryId(string id);
        int TotalProductByCategoryId(string id);
        IEnumerable<Product> GetProductFromMToN(int m, int n);
        IEnumerable<Product> GetAll();
        int TotalProduct();

        IEnumerable<Product> GetProductByPage(int page);


        int GetLastId();

        int? GetTotalQuantityOfAllProduct();


        //IEnumerable<SelectListItem> GetAllProductsForPayroll();
    }
}
