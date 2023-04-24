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
                Tax1 = x.Tax1,
                Tax2 = x.Tax2,
                Tax3 = x.Tax3,
                Total = x.Total,
                Quan = x.Quan,
                Status = x.Status,
                TransactId = x.TransactId,
                ProductNum = x.ProductNum
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
                Tax1 = transactdetail.Tax1,
                Tax2 = transactdetail.Tax2,
                Tax3 = transactdetail.Tax3,
                Total = transactdetail.Total,
                Quan = transactdetail.Quan,
                Status = transactdetail.Status,
                TransactId = transactdetail.TransactId,
                ProductNum = transactdetail.ProductNum
            };
            return Ok(model);
        }
        [HttpDelete("DeleteTransactdetailById")]
        public async Task<IActionResult> DeleteTransactdetailById(string id)
        {
            try
            {
                var transactdetail = _transactdetailService.GetById(id);
                var transactheader = _transactheaderService.GetById(transactdetail.TransactId);
                var total = transactdetail.Total;
                await _transactdetailService.DeleteById(id);

                transactheader.Net = (float.Parse(transactheader.Net) - float.Parse(total)).ToString();

                float totalRate = 0;

                var tax1 = _taxinfoService.GetById("T01");
                var tax2 = _taxinfoService.GetById("T02");
                var tax3 = _taxinfoService.GetById("T03");
                if (tax1 != null && transactheader.Tax1.Equals("1"))
                    totalRate += float.Parse(tax1.TaxRate);
                if (tax2 != null && transactheader.Tax2.Equals("1"))
                    totalRate += float.Parse(tax2.TaxRate);
                if (tax3 != null && transactheader.Tax3.Equals("1"))
                    totalRate += float.Parse(tax3.TaxRate);

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
            if (float.Parse(model.Total) < 0 || float.Parse(model.CostEach) < 0 || model.Quan < 0)
                return BadRequest();

            var transactdetail = _transactdetailService.GetById(model.TransactDetailId);
            if (transactdetail == null)
                return NotFound();

            transactdetail.CostEach = model.CostEach;
            transactdetail.Tax1 = model.Tax1;
            transactdetail.Tax2 = model.Tax2;
            transactdetail.Tax3 = model.Tax3;

            var transactheader = _transactheaderService.GetById(transactdetail.TransactId);
            transactheader.Net = (float.Parse(transactheader.Net) - float.Parse(transactdetail.Total)).ToString();
            transactdetail.Total = model.Total;
            transactheader.Net = (float.Parse(transactheader.Net) + float.Parse(transactdetail.Total)).ToString();
            transactdetail.Quan = model.Quan;
            transactdetail.Status = model.Status;

            float totalRate = 0;

            var tax1 = _taxinfoService.GetById("T01");
            var tax2 = _taxinfoService.GetById("T02");
            var tax3 = _taxinfoService.GetById("T03");
            if (tax1 != null && transactheader.Tax1.Equals("1"))
                totalRate += float.Parse(tax1.TaxRate);
            if (tax2 != null && transactheader.Tax2.Equals("1"))
                totalRate += float.Parse(tax2.TaxRate);
            if (tax3 != null && transactheader.Tax3.Equals("1"))
                totalRate += float.Parse(tax3.TaxRate);

            _transactheaderService.UpdateTotalCash(transactheader, totalRate);

            await _transactdetailService.UpdateAsSync(transactdetail);
            return Ok();
        }
        [HttpPost("CreateTransactdetail")]
        public async Task<IActionResult> CreateTransactdetail([FromBody] TransactdetailCreateViewModel model)
        {
            if (model == null)
                return BadRequest();
            if (float.Parse(model.Total) < 0 || float.Parse(model.CostEach) < 0 || model.Quan < 0)
                return BadRequest();

            var transactheader = _transactheaderService.GetById(model.TransactId);
            if (transactheader == null)
                return BadRequest();
            var product = _productService.GetById(model.ProductNum);
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
                Tax1 = model.Tax1,
                Tax2 = model.Tax2,
                Tax3 = model.Tax3,
                Total = model.Total,
                Quan = model.Quan,
                Status = model.Status,
                TransactId = model.TransactId,
                ProductNum = model.ProductNum
            };

            transactheader.Net = (float.Parse(transactheader.Total) + float.Parse(model.Total)).ToString();


            float totalRate = 0;

            var tax1 = _taxinfoService.GetById("T01");
            var tax2 = _taxinfoService.GetById("T02");
            var tax3 = _taxinfoService.GetById("T03");
            if (tax1 != null && transactheader.Tax1.Equals("1"))
                totalRate += float.Parse(tax1.TaxRate);
            if (tax2 != null && transactheader.Tax2.Equals("1"))
                totalRate += float.Parse(tax2.TaxRate);
            if (tax3 != null && transactheader.Tax3.Equals("1"))
                totalRate += float.Parse(tax3.TaxRate);

            _transactheaderService.UpdateTotalCash(transactheader, totalRate);

            await _transactdetailService.CreateAsSync(transactdetail);
            return Ok();
        }
    }
}
