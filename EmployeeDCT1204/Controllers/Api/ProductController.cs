using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("FilterProductPriceIncrease")]
        public IActionResult FilterProductPriceIncrease()
        {
            var products = _productService.FilterProductPriceIncrease().Select(product => new ProductIndexViewModel
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
            return Ok(products);
        }
        [HttpGet("FilterProductPriceDecrease")]
        public IActionResult FilterProductPriceDecrease()
        {
            var products = _productService.FilterProductPriceDecrease().Select(product => new ProductIndexViewModel
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
            return Ok(products);
        }
        [HttpGet("FilterProductByPrice")]
        public IActionResult FilterProductByPrice([FromQuery] ProductFilterViewModel model)
        {
            if (model.minPrice == null && model.maxPrice == null) return NotFound("Khong co tham so truyen vao!");
            float minPrice = 0, maxPrice = 0;
            try
            {
                if(model.minPrice ==null)
                    minPrice = 0;
                else
                    minPrice = float.Parse(model.minPrice.Trim());

                if (model.maxPrice == null)
                    maxPrice = 0;
                else
                    maxPrice = float.Parse(model.maxPrice.Trim());

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
            return Ok(products);
        }

        [HttpGet("FilterProductByName/{str}")]
        public IActionResult FilterProductByName(string str)
        {
            var model = _productService.FilterProductByName(str).Select(product => new ProductIndexViewModel
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

        [HttpGet("GetProductByName/{name}")]
        public IActionResult GetProductByName(string name)
        {
            var product = _productService.GetProductByName(name);
            if (product == null) return NotFound();
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

        [HttpGet("GetProductFromMToN")]
        public IActionResult GetProductFromMToN(int m, int n)
        {
            if (m <= 0 || n <= 0)
                return NotFound("m hoac n khong duoc nho hon hoac bang 0");
            if (m > n)
                return NotFound("m khong duoc lon hon n");
            var model = _productService.GetProductFromMToN(m, n).Select(product => new ProductDetailViewModel
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteById(id);
            return NoContent();
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductEditViewModel model)
        {
            if (model == null || model.ProductNum == null || model.CatId == null)
            {
                return BadRequest("Khong chua ProductNum");
            }
            
            var existingProduct = _productService.GetById(model.ProductNum);

            if (existingProduct == null)
            { 
                return NotFound("Khong tim thay product muon cap nhat !");
            }

            var category = _categoryService.GetById(model.CatId);
            if (category == null)
            {
                return BadRequest("Category id khong hop le !");
            }
            existingProduct.ProductName = model.ProductName != null && !model.ProductName.Trim().Equals("") ? model.ProductName : existingProduct.ProductName;
            existingProduct.Descript = model.Descript != null && !model.Descript.Trim().Equals("") ? model.Descript : existingProduct.Descript;
            existingProduct.Price = model.Price != null && !model.Price.Trim().Equals("") ? model.Price : existingProduct.Price;
            existingProduct.Tax1 = model.Tax1 != null && !model.Tax1.Trim().Equals("") ? model.Tax1 : existingProduct.Tax1;
            existingProduct.Tax2 = model.Tax2 != null && !model.Tax2.Trim().Equals("") ? model.Tax2 : existingProduct.Tax2;
            existingProduct.Tax3 = model.Tax3 != null && !model.Tax3.Trim().Equals("") ? model.Tax3 : existingProduct.Tax3;
            existingProduct.Quan = model.Quan != null && !model.Quan.Trim().Equals("") && float.Parse(model.Quan) >= 0 ? model.Quan : existingProduct.Quan;
            existingProduct.ImageSource = model.ImageSource != null && !model.ImageSource.Trim().Equals("") ? model.ImageSource : existingProduct.ImageSource;
            existingProduct.CatId = model.CatId;
            existingProduct.Category = category;

            await _productService.UpdateAsSync(existingProduct);

            return NoContent();
        }

        [HttpPost("CreateProduct")]
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

