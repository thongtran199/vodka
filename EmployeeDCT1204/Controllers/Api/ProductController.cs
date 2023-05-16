using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Versioning;
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
                ProductId = product.ProductId,
                Name = product.Name,
                Descript = product.Descript,
                Price = product.Price,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CategoryId = product.CategoryId,
                Category = null,
                TransactDetails = null
            }).ToList();
            return Ok(model);
        }

        [HttpGet("GetAllProductsIncreaseByPrice")]
        public IActionResult GetAllProductsIncreaseByPrice()
        {
            var model = _productService.GetAll()
                .OrderBy(p => p.Price)
                .Select(product => new ProductIndexViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Descript = product.Descript,
                Price = product.Price,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CategoryId = product.CategoryId,
                Category = null,
                TransactDetails = null
            }).ToList();
            return Ok(model);
        }
        [HttpGet("GetAllProductsDecreaseByPrice")]
        public IActionResult GetAllProductsDecreaseByPrice()
        {
            var model = _productService.GetAll()
                .OrderByDescending(p => p.Price)
                .Select(product => new ProductIndexViewModel
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Descript = product.Descript,
                    Price = product.Price,
                    Quan = product.Quan,
                    IsActive = product.IsActive,
                    ImageSource = product.ImageSource,
                    CategoryId = product.CategoryId,
                    Category = null,
                    TransactDetails = null
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
                ProductId = product.ProductId,
                Name = product.Name,
                Descript = product.Descript,
                Price = product.Price,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CategoryId = product.CategoryId,
                Category = null,
                TransactDetails = null
            };
            return Ok(model);
        }

        [HttpGet("FilterProductByPrice")]
        public IActionResult FilterProductByPrice([FromQuery] ProductFilterViewModel model)
        {
            if (model.minPrice == null && model.maxPrice == null) return NotFound("Khong co tham so truyen vao!");
            decimal minPrice = 0, maxPrice = 0;
            try
            {
                if(model.minPrice == null)
                    minPrice = 0;
                else
                    minPrice = model.minPrice;

                if (model.maxPrice == null)
                    maxPrice = 0;
                else
                    maxPrice = model.maxPrice;

            }
            catch (Exception ex)
            {
                return NotFound("Tham so truyen vao khong phai so");
            }
            if ((minPrice > maxPrice && maxPrice != 0) || maxPrice < 0)
            {
                return NotFound("minPrice khong the > maxPrice, maxPrice phai > 0 !");
            }
            var products = _productService.FilterProductByPrice(minPrice, maxPrice).Select( product => new ProductIndexViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Descript = product.Descript,
                Price = product.Price,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CategoryId = product.CategoryId,
                Category = null,
                TransactDetails = null
            }).ToList();
            return Ok(products);
        }

        [HttpGet("FilterProductByName/{str}")]
        public IActionResult FilterProductByName(string str)
        {
            var model = _productService.FilterProductByName(str).Select(product => new ProductIndexViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Descript = product.Descript,
                Price = product.Price,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CategoryId = product.CategoryId,
                Category = null,
                TransactDetails = null
            }).ToList();
            return Ok(model);
        }

        [HttpGet("GetProductByName/{name}")]
        public IActionResult GetProductByName(string name)
        {
            var product = _productService.GetProductByName(name);
            if (product == null) return NotFound();
            var model = new ProductDetailViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Descript = product.Descript,
                Price = product.Price,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CategoryId = product.CategoryId,
                Category = null,
                TransactDetails = null
            };
            return Ok(model);
        }

        [HttpGet("TotalFilterProductByName/{str}")]
        public IActionResult TotalFilterProductByName(string str)
        {
            var num = _productService.TotalFilterProductByName(str);
            if (num == null) return NotFound();
            return Ok(num);
        }

        [HttpGet("TotalProductFilterByPrice")]
        public IActionResult TotalFilterProductByPrice([FromQuery] ProductFilterViewModel model)
        {
            var num = _productService.TotalProductFilterByPrice(model.minPrice, model.maxPrice);
            if (num == null) return NotFound();
            return Ok(num);
        }

        [HttpGet("TotalProductByCategoryId")]
        public IActionResult TotalProductByCategoryId(string id)
        {
            var num = _productService.TotalProductByCategoryId(id);
            if (num == null) return NotFound();
            return Ok(num);
        }

        [HttpGet("TotalNumberOfProduct")]
        public IActionResult TotalGetAll()
        {
            var num = _productService.TotalProduct();
            if (num == null) return NotFound();
            return Ok(num);
        }



        [HttpGet("GetProductsByCategoryId")]
        public IActionResult GetProductsByCategoryId(string id)
        {
            var products = _productService.GetProductsByCategoryId(id).Select(product => new ProductDetailViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Descript = product.Descript,
                Price = product.Price,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CategoryId = product.CategoryId,
                Category = null,
                TransactDetails = null
            }).ToList();
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("GetProductFromMToN")]
        public IActionResult GetProductFromMToN(int m, int n)
        {
            if (m <= 0 || n <= 0)
                return NotFound("m hoac n khong duoc nho hon hoac bang 0");
            if (m > n)
                return NotFound("m khong duoc lon hon n");
            var model = _productService.GetProductFromMToN(m, n).Select(product => new ProductDetailViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Descript = product.Descript,
                Price = product.Price,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CategoryId = product.CategoryId,
                Category = null,
                TransactDetails = null
            }).ToList();

            return Ok(model);
        }

        [HttpGet("GetProductByPage")]
        public IActionResult GetProductByPage(int page)
        {
            var model = _productService.GetProductByPage(page).Select(product => new ProductDetailViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Descript = product.Descript,
                Price = product.Price,
                Quan = product.Quan,
                IsActive = product.IsActive,
                ImageSource = product.ImageSource,
                CategoryId = product.CategoryId,
                Category = null,
                TransactDetails = null
            }).ToList();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteById(id);
            return NoContent();
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductEditViewModel model)
        {
            if (model == null || model.ProductId == null || model.CategoryId == null)
            {
                return BadRequest("Khong chua ProductId");
            }
            
            var existingProduct = _productService.GetById(model.ProductId);

            if (existingProduct == null)
            { 
                return NotFound("Khong tim thay product muon cap nhat !");
            }

            var category = _categoryService.GetById(model.CategoryId);
            if (category == null)
            {
                return BadRequest("Category id khong hop le !");
            }
            existingProduct.Name = model.Name != null && !model.Name.Trim().Equals("") ? model.Name : existingProduct.Name;
            existingProduct.Descript = model.Descript != null && !model.Descript.Trim().Equals("") ? model.Descript : existingProduct.Descript;
            existingProduct.Price = model.Price != null && model.Price > 0 ? model.Price : existingProduct.Price;
            existingProduct.Quan = model.Quan != null && model.Quan >= 0 ? model.Quan : existingProduct.Quan;
            existingProduct.ImageSource = model.ImageSource != null && !model.ImageSource.Trim().Equals("") ? model.ImageSource : existingProduct.ImageSource;
            existingProduct.CategoryId = model.CategoryId;
            existingProduct.Category = category;

            await _productService.UpdateAsSync(existingProduct);

            return NoContent();
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (String.IsNullOrEmpty(model.CategoryId))
                return BadRequest("Khong co CategoryId");

            var category = _categoryService.GetById(model.CategoryId);
            if (category == null)
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
                ProductId = new_str_id,
                Name = model.Name,
                Descript = model.Descript,
                Price = model.Price,
                Quan = model.Quan,
                IsActive = 1,
                ImageSource = model.ImageSource,
                TransactDetails = null,
                CategoryId = model.CategoryId,
                Category = null
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



        [HttpGet("GetTotalPage")]
        public IActionResult GetTotalPage()
        {
            var model = _productService.GetAll().Count();
            return Ok(model/12);
        }
        [HttpPut("UpdateQuantityAsync")]
        public async Task<ActionResult> UpdateQuantityAsync(string productId, int quantity)
        {
            var result = await _productService.UpdateQuantityAsync(productId, quantity);
            if (result.Succeeded)
            {
                return Ok("Cap nhat thanh cong !");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [HttpPut("UpdateIsActiveAsync")]
        public async Task<ActionResult> UpdateIsActiveAsync(string productId, int isActive)
        {
            var result = await _productService.UpdateIsActiveAsync(productId, isActive);
            if (result.Succeeded)
            {
                return Ok("Cap nhat thanh cong !");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("GetTotalQuantityOfAllProduct")]
        public IActionResult GetTotalQuantityOfAllProduct()
        {
            var result = _productService.GetTotalQuantityOfAllProduct();
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}

