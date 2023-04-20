using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Vodka.Models.Category;
using VodkaEntities;
using VodkaServices;
using VodkaServices.Implementation;

namespace Vodka.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategories")]
        public IActionResult GetAll()
        {
            var categoryList = _categoryService.GetAll().Select(x => new CategoryIndexViewModel
            {
                CatId = x.CatId,
                CatName = x.CatName,
                Descript = x.Descript,
                IsActive = x.IsActive
            }).ToList();
            return Ok(categoryList);
        }
        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetCategoryById(string id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound();
            var model = new CategoryDetailViewModel
            {
                CatId = category.CatId,
                CatName = category.CatName,
                Descript = category.Descript,
                IsActive = category.IsActive
            };
            return Ok(model);
        }
        [HttpDelete("DeleteCategoryById")]
        public async Task<IActionResult> DeleteCategoryById(string id)
        {
            try
            {
                await _categoryService.DeleteById(id);
            }
            catch(Exception ex)
            {
                return BadRequest("Id danh muc khong hop le");
            }
            return NoContent();
                
        }
        [HttpPut("UpdateAsSync")]
        public async Task<IActionResult> UpdateAsSync([FromBody]CategoryEditViewModel model)
        {
            if (model == null)
                return BadRequest();
            var category = _categoryService.GetById(model.CatId);
            if (category == null)
                return NotFound();
            category.CatName = model.CatName;
            category.Descript = model.Descript;

            await _categoryService.UpdateAsSync(category);
            return Ok();
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateViewModel model)
        {
            if (model == null)
                return BadRequest();

            string new_str_id = "";
            int new_int_id = _categoryService.GetLastId() + 1;
            if (new_int_id >= 100 && new_int_id < 1000)
                new_str_id = "C00" + new_int_id.ToString();
            else if (new_int_id < 10 && new_int_id >= 0)
                new_str_id = "C0000" + new_int_id.ToString();
            else if (new_int_id < 100 && new_int_id >= 10)
                new_str_id = "C000" + new_int_id.ToString();

            var category = new Category
            {
                CatId = new_str_id,
                CatName = model.CatName,
                Descript = model.Descript,
                IsActive = "1",
                Product = null
            };

            await _categoryService.CreateAsSync(category);
            return Ok();
        }
    }
}
