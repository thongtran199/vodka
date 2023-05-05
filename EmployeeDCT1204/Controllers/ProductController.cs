using Vodka.Models;
using VodkaServices;
using Microsoft.AspNetCore.Mvc;
using VodkaEntities;
using Microsoft.EntityFrameworkCore;
using VodkaDataAccess;
using Vodka.Models.Product;

namespace ProductDCT1204.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IWebHostEnvironment _hostingEnvironment;
        public ProductController(IProductService productService, IWebHostEnvironment hostingEnvironment)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var model = _productService.GetAll().Select(product => new ProductIndexViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Descript = product.Descript,
                Price = product.Price,
                ImageSource = product.ImageSource
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductCreateViewModel();
            return View(model);
        }


        [HttpGet]
        public IActionResult Detail(string id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var model = new ProductDetailViewModel();
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id.ToString() == null)
                return NotFound();
            var model = new ProductDeleteViewModel();
            return View(model);
        }

 
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id.ToString() == null)
                return NotFound();
            var model = new ProductEditViewModel();
            return View(model);
        }
    }
}

