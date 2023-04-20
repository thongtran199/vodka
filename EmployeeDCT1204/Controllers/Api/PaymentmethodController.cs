using Microsoft.AspNetCore.Mvc;
using Vodka.Models.Paymentmethod;
using VodkaEntities;
using VodkaServices;

namespace Vodka.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentmethodController : ControllerBase
    {
        private IPaymentmethodService _paymentmethodService;
        public PaymentmethodController(IPaymentmethodService paymentmethodService)
        {
            _paymentmethodService = paymentmethodService;
        }

        [HttpGet("GetAllPaymentmethods")]
        public IActionResult GetAll()
        {
            var paymentmethodList = _paymentmethodService.GetAll().Select(x => new PaymentmethodIndexViewModel
            {
                PaymentId = x.PaymentId,
                PaymentName = x.PaymentName,
                Descript = x.Descript
            }).ToList();
            return Ok(paymentmethodList);
        }
        [HttpGet("GetPaymentmethodById/{id}")]
        public IActionResult GetPaymentmethodById(string id)
        {
            var paymentmethod = _paymentmethodService.GetById(id);
            if (paymentmethod == null)
                return NotFound();
            var model = new PaymentmethodDetailViewModel
            {
                PaymentId = paymentmethod.PaymentId,
                PaymentName = paymentmethod.PaymentName,
                Descript = paymentmethod.Descript
            };
            return Ok(model);
        }
        [HttpDelete("DeletePaymentmethodById")]
        public async Task<IActionResult> DeletePaymentmethodById(string id)
        {
            try
            {
                await _paymentmethodService.DeleteById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Id danh muc khong hop le");
            }
            return NoContent();

        }
        [HttpPut("UpdateAsSync")]
        public async Task<IActionResult> UpdateAsSync([FromBody] PaymentmethodEditViewModel model)
        {
            if (model == null)
                return BadRequest();
            var paymentmethod = _paymentmethodService.GetById(model.PaymentId);
            if (paymentmethod == null)
                return NotFound();
            paymentmethod.PaymentName = model.PaymentName;
            paymentmethod.Descript = model.Descript;

            await _paymentmethodService.UpdateAsSync(paymentmethod);
            return Ok();
        }
        [HttpPost("CreatePaymentmethod")]
        public async Task<IActionResult> CreatePaymentmethod([FromBody] PaymentmethodCreateViewModel model)
        {
            if (model == null)
                return BadRequest();

            string new_str_id = "";
            int new_int_id = _paymentmethodService.GetLastId() + 1;
            if (new_int_id >= 100 && new_int_id < 1000)
                new_str_id = "P00" + new_int_id.ToString();
            else if (new_int_id < 10 && new_int_id >= 0)
                new_str_id = "P0000" + new_int_id.ToString();
            else if (new_int_id < 100 && new_int_id >= 10)
                new_str_id = "P000" + new_int_id.ToString();

            var paymentmethod = new Paymentmethod
            {
                PaymentId = new_str_id,
                PaymentName = model.PaymentName,
                Descript = model.Descript
            };

            await _paymentmethodService.CreateAsSync(paymentmethod);
            return Ok();
        }
    }
}
