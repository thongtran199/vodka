using Microsoft.AspNetCore.Mvc;
using Vodka.Models.Taxinfo;
using VodkaEntities;
using VodkaServices;

namespace Vodka.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxinfoController : ControllerBase
    {
        private ITaxinfoService _taxinfoService;
        public TaxinfoController(ITaxinfoService taxinfoService)
        {
            _taxinfoService = taxinfoService;
        }

        [HttpGet("GetAllTaxinfos")]
        public IActionResult GetAll()
        {
            var taxinfoList = _taxinfoService.GetAll().Select(x => new TaxinfoIndexViewModel
            {
                TaxId = x.TaxId,
                Descript = x.Descript,
                Rate = x.Rate
            }).ToList();
            return Ok(taxinfoList);
        }
        [HttpGet("GetTaxInfoById/{id}")]
        public IActionResult GettaxinfoById(string id)
        {
            var taxinfo = _taxinfoService.GetById(id);
            if (taxinfo == null)
                return NotFound();
            var model = new TaxinfoDetailViewModel
            {
                TaxId = taxinfo.TaxId,
                Descript = taxinfo.Descript,
                Rate = taxinfo.Rate
            };
            return Ok(model);
        }
        [HttpDelete("DeleteTaxinfoById")]
        public async Task<IActionResult> DeletetaxinfoById(string id)
        {
            try
            {
                await _taxinfoService.DeleteById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Id danh muc khong hop le");
            }
            return NoContent();

        }
        [HttpPut("UpdateAsSync")]
        public async Task<IActionResult> UpdateAsSync([FromBody] TaxinfoEditViewModel model)
        {
            if (model == null)
                return BadRequest();
            if (model.Rate < 0)
                return BadRequest();

            var taxinfo = _taxinfoService.GetById(model.TaxId);
            if (taxinfo == null)
                return NotFound();
            taxinfo.Descript = model.Descript;
            taxinfo.Rate = model.Rate;

            await _taxinfoService.UpdateAsSync(taxinfo);
            return Ok();
        }
        [HttpPost("CreateTaxinfo")]
        public async Task<IActionResult> CreateTaxinfo([FromBody] TaxinfoCreateViewModel model)
        {
            if (model == null)
                return BadRequest();
            if (model.Rate < 0)
                return BadRequest();

            string new_str_id = "";
            int new_int_id = _taxinfoService.GetLastId() + 1;
            if (new_int_id >= 100 && new_int_id < 1000)
                new_str_id = "T" + new_int_id.ToString();
            else if (new_int_id < 10 && new_int_id >= 0)
                new_str_id = "T0" + new_int_id.ToString();
            else if (new_int_id < 100 && new_int_id >= 10)
                new_str_id = "T" + new_int_id.ToString();

            var taxinfo = new Taxinfo
            {
                TaxId = new_str_id,
                Descript = model.Descript,
                Rate = model.Rate
            };

            await _taxinfoService.CreateAsSync(taxinfo);
            return Ok();
        }
    }
}
