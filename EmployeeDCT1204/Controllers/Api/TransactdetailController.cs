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

                var product = _productService.GetById(transactdetail.ProductId);
                if (product == null)
                    return BadRequest();

                product.Quan += transactdetail.Quan;
                
                var total = transactdetail.Total;
                await _transactdetailService.DeleteById(id);

                transactheader.Net = transactheader.Net - total;

                await _transactheaderService.UpdateTotalCash(transactheader, 30);
                await _productService.UpdateAsSync(product);

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

            var product = _productService.GetById(transactdetail.ProductId);
            if (product == null)
                return BadRequest();

            product.Quan += transactdetail.Quan;
            product.Quan -= model.Quan;

            transactdetail.CostEach = model.CostEach;
            transactdetail.Quan = model.Quan;


            var transactheader = _transactheaderService.GetById(transactdetail.TransactHeaderId);
            transactheader.Net = transactheader.Net - transactdetail.Total;

            transactdetail.Total = model.Total;

            transactheader.Net = transactheader.Net + transactdetail.Total;

            _transactheaderService.UpdateTotalCash(transactheader, 30);

            await _transactdetailService.UpdateAsSync(transactdetail);
            await _productService.UpdateAsSync(product);
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
            
            product.Quan = product.Quan - model.Quan;

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

            Console.WriteLine("SO LUONG SAN PHAM: ", product.Quan.ToString());
            Console.WriteLine("NET HEADER: ", transactheader.Net.ToString());



            await _transactheaderService.UpdateTotalCash(transactheader, 30);
            await _productService.UpdateAsSync(product);
            await _transactdetailService.CreateAsSync(transactdetail);
            return Ok();
        }

        [HttpPost("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity(string id, int newQuantity)
        {
            
            var transactdetail = _transactdetailService.GetById(id);
            if (transactdetail == null)
                return NotFound();

            var transactheader = _transactheaderService.GetById(transactdetail.TransactHeaderId);
            if (transactheader == null)
                return NotFound();

            var product = _productService.GetById(transactdetail.ProductId);
            if (product == null)
                return NotFound();

            product.Quan += transactdetail.Quan;
            product.Quan -= newQuantity;

            transactheader.Net = transactheader.Net - transactdetail.Total;
            transactdetail.Total = transactdetail.CostEach * newQuantity;
            transactheader.Net = transactheader.Net + transactdetail.Total;

            await _transactdetailService.UpdateQuantity(id, newQuantity);
            await _transactheaderService.UpdateTotalCash(transactheader, 30);
            await _productService.UpdateAsSync(product);

            return Ok();

        }

        [HttpGet("GetTransactdetailsByTransactheaderId/{id}")]
        public IActionResult GetTransactdetailByTransactheaderId(string id)
        {
            var transactdetails = _transactdetailService.GetTransactdetailsByTransactheaderId(id);
            if (transactdetails == null)
                return NotFound();
            return Ok(transactdetails);
        }
    }
}
