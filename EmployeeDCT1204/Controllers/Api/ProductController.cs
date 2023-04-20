using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Vodka.Models.Product;
using VodkaEntities;
using VodkaServices;
namespace Vodka.Controllers.Api
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var model = _productService.GetAll().Select(product => new ProductIndexViewModel
            {
                ProductNum = product.ProductNum,
                ProductName = product.ProductName,
                Descript = product.Descript,
                Price = product.Price,
                Tax1 = product.Tax1,
                Tax2 = product.Tax2,
                Tax3 = product.Tax3,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CatId = product.CatId,
                Category = null,
                Transactdetail = null
            }).ToList();
            return Ok(model);
        }

        [HttpGet("GetProductById/{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetProductById(string id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductDetailViewModel
            {
                ProductNum = product.ProductNum,
                ProductName = product.ProductName,
                Descript = product.Descript,
                Price = product.Price,
                Tax1 = product.Tax1,
                Tax2 = product.Tax2,
                Tax3 = product.Tax3,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CatId = product.CatId,
                Category = null,
                Transactdetail = null
            };
            return Ok(model);
        }

        [HttpGet("GetProductsByCategoryId")]
        public IActionResult GetProductsByCategoryId(string id)
        {
            var products = _productService.GetProductsByCategoryId(id).Select(product => new ProductDetailViewModel
            {
                ProductNum = product.ProductNum,
                ProductName = product.ProductName,
                Descript = product.Descript,
                Price = product.Price,
                Tax1 = product.Tax1,
                Tax2 = product.Tax2,
                Tax3 = product.Tax3,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CatId = product.CatId,
                Category = null,
                Transactdetail = null
            }).ToList();
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteById(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] ProductEditViewModel model)
        {
            if (model == null || !id.Equals(model.ProductNum) || int.Parse(model.Quan) < 0)
            {
                return BadRequest();
            }

            var existingProduct = _productService.GetById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetById(model.CatId);
            if (category == null)
            {
                return BadRequest("Category id khong hop le !");
            }
            existingProduct.ProductName = model.ProductName;
            existingProduct.Descript = model.Descript;
            existingProduct.Price = model.Price;
            existingProduct.Tax1 = model.Tax1;
            existingProduct.Tax2 = model.Tax2;
            existingProduct.Tax3 = model.Tax3;
            existingProduct.Quan = model.Quan;
            existingProduct.ImageSource = model.ImageSource;
            existingProduct.CatId = model.CatId;
            existingProduct.Category = category;

            await _productService.UpdateAsSync(existingProduct);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]ProductCreateViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            //Kiểm tra catId có hợp lệ hay không
            var category = _categoryService.GetById(model.CatId);
            if(category == null)
            {
                return BadRequest("Category id khong hop le !");
            }
            string new_str_id = "";
            int new_int_id = _productService.GetLastId() + 1;
            if (new_int_id >= 100 && new_int_id < 1000)
                new_str_id = "P00" + new_int_id.ToString();
            else if(new_int_id < 10 && new_int_id >= 0)
                new_str_id = "P0000" + new_int_id.ToString();
            else if (new_int_id < 100 && new_int_id >= 10)
                new_str_id = "P000" + new_int_id.ToString();

            var newProduct = new Product
            {
                ProductNum = new_str_id,
                ProductName = model.ProductName,
                Descript = model.Descript,
                Price = model.Price,
                Tax1 = model.Tax1,
                Tax2 = model.Tax2,
                Tax3 = model.Tax3,
                Quan = model.Quan,
                IsActive = "1",
                ImageSource = model.ImageSource,
                Transactdetail = null,
                CatId = model.CatId,
                Category = category
            };
            try
            {
                await _productService.CreateAsSync(newProduct);
            }
            catch (Exception Ex)
            {
                return BadRequest();
            }
            return Ok("Them san pham thanh cong");
        }

    }
    }

