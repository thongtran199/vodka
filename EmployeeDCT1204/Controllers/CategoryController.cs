using VodkaDataAccess;
using Vodka.Models.Category;
using VodkaServices;
using Microsoft.AspNetCore.Mvc;
using VodkaEntities;
namespace EmployeeDCT1204.Controllers
{
    public class CategoryController:Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _categoryService.GetAll().Select(category => new CategoryIndexViewModel
            {
                CatId = category.CatId,
                CatName = category.CatName,
                Descript = category.Descript,
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Detail(string id)
        {
            if (id.ToString() == null)
                return NotFound();
            var model = new CategoryDetailViewModel();
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id.ToString() == null)
                return NotFound();
            var model = new CategoryEditViewModel();
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id.ToString() == null)
                return NotFound();
            var model = new CategoryDeleteViewModel();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CategoryCreateViewModel();  
            return View(model);
        }
    }
}
