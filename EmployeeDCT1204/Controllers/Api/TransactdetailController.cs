using Microsoft.AspNetCore.Mvc;
using Vodka.Models.Transactdetail;
using VodkaEntities;
using VodkaServices;

namespace Vodka.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactdetailController : ControllerBase
    {
        private ITransactdetailService _transactdetailService;
        private ITransactheaderService _transactheaderService;
        private IProductService _productService;
        private ITaxinfoService _taxinfoService;
        public TransactdetailController(ITaxinfoService taxinfoService, ITransactdetailService transactdetailService, ITransactheaderService transactheaderService, IProductService productService)
        {
            _transactdetailService = transactdetailService;
            _transactheaderService = transactheaderService;
            _productService = productService;
            _taxinfoService = taxinfoService;
        }

        [HttpGet("GetAllTransactdetails")]
        public IActionResult GetAll()
        {
            var transactdetailList = _transactdetailService.GetAll().Select(x => new TransactdetailIndexViewModel
            {
                TransactDetailId = x.TransactDetailId,
                CostEach = x.CostEach,
                Total = x.Total,
                Quan = x.Quan,
                TransactHeaderId = x.TransactHeaderId,
                ProductId = x.ProductId
            }).ToList();
            return Ok(transactdetailList);
        }
        [HttpGet("GetTransactdetailById/{id}")]
        public IActionResult GetTransactdetailById(string id)
        {
            var transactdetail = _transactdetailService.GetById(id);
            if (transactdetail == null)
                return NotFound();
            var model = new TransactdetailDetailViewModel
            {
                TransactDetailId = transactdetail.TransactDetailId,
                CostEach = transactdetail.CostEach,

                Total = transactdetail.Total,
                Quan = transactdetail.Quan,
                TransactHeaderId = transactdetail.TransactHeaderId,
                ProductId = transactdetail.ProductId
            };
            return Ok(model);
        }
        [HttpDelete("DeleteTransactdetailById")]
        public async Task<IActionResult> DeleteTransactdetailById(string id)
        {
            try
            {
                var transactdetail = _transactdetailService.GetById(id);
                var transactheader = _transactheaderService.GetById(transactdetail.TransactHeaderId);
                var total = transactdetail.Total;
                await _transactdetailService.DeleteById(id);

                transactheader.Net = transactheader.Net - total;

                decimal totalRate = 0;

                var tax1 = _taxinfoService.GetById("T01");
                var tax2 = _taxinfoService.GetById("T02");
                var tax3 = _taxinfoService.GetById("T03");
                if (tax1 != null && transactheader.Tax1 == 1)
                    totalRate = totalRate + tax1.Rate;
                if (tax2 != null && transactheader.Tax2 == 1)
                    totalRate += tax2.Rate;
                if (tax3 != null && transactheader.Tax3 == 1)
                    totalRate += tax3.Rate;

                _transactheaderService.UpdateTotalCash(transactheader, totalRate);

            }
            catch (Exception ex)
            {
                return BadRequest("Transactdetail ID không hợp lệ !");
            }
            return Ok();

        }
        [HttpPut("UpdateAsSync")]
        public async Task<IActionResult> UpdateAsSync([FromBody] TransactdetailEditViewModel model)
        {
            if (model == null)
                return BadRequest();
            if (model.Total < 0 || model.CostEach < 0 || model.Quan < 0)
                return BadRequest();

            var transactdetail = _transactdetailService.GetById(model.TransactDetailId);
            if (transactdetail == null)
                return NotFound();

            transactdetail.CostEach = model.CostEach;


            var transactheader = _transactheaderService.GetById(transactdetail.TransactHeaderId);
            transactheader.Net = transactheader.Net - transactdetail.Total;
            transactdetail.Total = model.Total;
            transactheader.Net = transactheader.Net + transactdetail.Total;
            transactdetail.Quan = model.Quan;

            decimal totalRate = 0;

            var tax1 = _taxinfoService.GetById("T01");
            var tax2 = _taxinfoService.GetById("T02");
            var tax3 = _taxinfoService.GetById("T03");
            if (tax1 != null && transactheader.Tax1 == 1)
                totalRate = totalRate + tax1.Rate;
            if (tax2 != null && transactheader.Tax2 == 1)
                totalRate += tax2.Rate;
            if (tax3 != null && transactheader.Tax3 == 1)
                totalRate += tax3.Rate;

            _transactheaderService.UpdateTotalCash(transactheader, totalRate);

            await _transactdetailService.UpdateAsSync(transactdetail);
            return Ok();
        }
        [HttpPost("CreateTransactdetail")]
        public async Task<IActionResult> CreateTransactdetail([FromBody] TransactdetailCreateViewModel model)
        {
            if (model == null)
                return BadRequest();
            if (model.Total < 0 || model.CostEach < 0 || model.Quan < 0)
                return BadRequest();

            var transactheader = _transactheaderService.GetById(model.TransactHeaderId);
            if (transactheader == null)
                return BadRequest();
            var product = _productService.GetById(model.ProductId);
            if (product == null)
                return BadRequest();

            string new_str_id = "";
            int new_int_id = _transactdetailService.GetLastId() + 1;
            if (new_int_id >= 100 && new_int_id < 1000)
                new_str_id = "TD00" + new_int_id.ToString();
            else if (new_int_id < 10 && new_int_id >= 0)
                new_str_id = "TD0000" + new_int_id.ToString();
            else if (new_int_id < 100 && new_int_id >= 10)
                new_str_id = "TD000" + new_int_id.ToString();

            var transactdetail = new Transactdetail
            {
                TransactDetailId = new_str_id,
                CostEach = model.CostEach,
                Total = model.Total,
                Quan = model.Quan,
                TransactHeaderId = model.TransactHeaderId,
                ProductId = model.ProductId
            };

            transactheader.Net = transactheader.Total + model.Total;


            decimal totalRate = 0;

            var tax1 = _taxinfoService.GetById("T01");
            var tax2 = _taxinfoService.GetById("T02");
            var tax3 = _taxinfoService.GetById("T03");
            if (tax1 != null && transactheader.Tax1 == 1)
                totalRate = totalRate + tax1.Rate;
            if (tax2 != null && transactheader.Tax2 == 1)
                totalRate += tax2.Rate;
            if (tax3 != null && transactheader.Tax3 == 1)
                totalRate += tax3.Rate;

            _transactheaderService.UpdateTotalCash(transactheader, totalRate);

            await _transactdetailService.CreateAsSync(transactdetail);
            return Ok();
        }
    }
}
