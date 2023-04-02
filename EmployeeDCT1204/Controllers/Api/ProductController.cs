using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Vodka.Models.Product;
using VodkaServices;
namespace Vodka.Controllers.Api
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase{
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var model = _productService.GetAll().Select(product => new ProductIndexViewModel
            {
                ProductNum = product.ProductNum,
                ProductName = product.ProductName,
                Descript = product.Descript,
                Price = product.Price,
                ImageSource = product.ImageSource
            }).ToList();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(string id)
        {
            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        {
            _productService.DeleteById(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] ProductEditViewModel model)
        {
            if (model == null || !id.Equals(model.ProductNum))
            {
                return BadRequest();
            }

            var existingProduct = _productService.GetById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.ProductName = model.ProductName;
            existingProduct.Descript = model.Descript;
            existingProduct.Price = model.Price;
            existingProduct.ImageSource = model.ImageSource;

            await _productService.UpdateAsSync(existingProduct);

            return NoContent();
        }
    }
    }

