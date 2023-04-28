using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Vodka.Models.Transactheader;
using VodkaEntities;
using VodkaServices;

namespace Vodka.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactheaderController : ControllerBase
    {
        private ITransactdetailService _transactdetailService;
        private ITransactheaderService _transactheaderService;
        private IUseraccountService _useraccountService;
        private IProductService _productService;
        public TransactheaderController(ITransactdetailService transactdetailService, ITransactheaderService transactheaderService, IUseraccountService useraccountService, IProductService productService)
        {
            _transactheaderService = transactheaderService;
            _useraccountService = useraccountService;
            _productService = productService;
            _transactdetailService = transactdetailService;
        }

        [HttpGet("GetAllTransactheaders")]
        public IActionResult GetAll()
        {
            var transactheaderList = _transactheaderService.GetAll().Select(x => new TransactheaderIndexViewModel
            {
                TransactId = x.TransactId,
                Net = x.Net,
                Tax1 = x.Tax1,
                Tax2 = x.Tax2,
                Tax3 = x.Tax3,
                Total = x.Total,
                TimePayment = x.TimePayment,
                WhoPay = x.WhoPay,
                Status = x.Status
            }).ToList();
            return Ok(transactheaderList);
        }
        [HttpGet("GetTransactheaderById/{id}")]
        public IActionResult GetTransactheaderById(string id)
        {
            var transactheader = _transactheaderService.GetById(id);
            if (transactheader == null)
                return NotFound();
            var model = new TransactheaderDetailViewModel
            {
                TransactId = transactheader.TransactId,
                Net = transactheader.Net,
                Tax1 = transactheader.Tax1,
                Tax2 = transactheader.Tax2,
                Tax3 = transactheader.Tax3,
                Total = transactheader.Total,
                TimePayment = transactheader.TimePayment,
                WhoPay = transactheader.WhoPay,
                Status = transactheader.Status
            };
            return Ok(model);
        }

        [HttpGet("GetTransactheadersByUserId/{userId}")]
        public IActionResult GetTransactheadersByUserId(string userId)
        {
            var transactheaders = _transactheaderService.GetTransactheadersByUserId(userId)
                                    .Select(x => new TransactheaderIndexViewModel
                                    {
                                        TransactId = x.TransactId,
                                        Net = x.Net,
                                        Tax1 = x.Tax1,
                                        Tax2 = x.Tax2,
                                        Tax3 = x.Tax3,
                                        Total = x.Total,
                                        TimePayment = x.TimePayment,
                                        WhoPay = x.WhoPay,
                                        Status = x.Status
                                    }).ToList();
            return Ok(transactheaders); 
        }
        [HttpDelete("DeleteTransactheaderById")]
        public async Task<IActionResult> DeleteTransactheaderById(string id)
        {
            try
            {
                await _transactheaderService.DeleteById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Transactheader ID không hợp lệ");
            }
            return Ok();

        }
        [HttpPut("UpdateAsSync")]
        public async Task<IActionResult> UpdateAsSync([FromBody] TransactheaderEditViewModel model)
        {
            if (model == null)
                return BadRequest();
            var transactheader = _transactheaderService.GetById(model.TransactId);
            if (transactheader == null)
                return NotFound();
            transactheader.Net = model.Net;
            transactheader.Tax1 = model.Tax1;
            transactheader.Tax2 = model.Tax2;
            transactheader.Tax3 = model.Tax3;
            transactheader.Total = model.Total;
            transactheader.TimePayment = model.TimePayment;
            transactheader.Status = model.Status;

            await _transactheaderService.UpdateAsSync(transactheader);
            return Ok();
        }
        [HttpPost("CreateTransactheader")]
        public async Task<IActionResult> CreateTransactheader([FromBody] TransactheaderCreateViewModel model)
        {
            if (model == null)
                return BadRequest();
            if (float.Parse(model.Total) < 0)
                return BadRequest();

            var whoPay = _useraccountService.GetById(model.WhoPay);
            if (whoPay == null)
                return NotFound();

            string new_str_id = "";
            int new_int_id = _transactheaderService.GetLastId() + 1;
            if (new_int_id >= 100 && new_int_id < 1000)
                new_str_id = "TS00" + new_int_id.ToString();
            else if (new_int_id < 10 && new_int_id >= 0)
                new_str_id = "TS0000" + new_int_id.ToString();
            else if (new_int_id < 100 && new_int_id >= 10)
                new_str_id = "TS000" + new_int_id.ToString();

            var transactheader = new Transactheader
            {
                TransactId = new_str_id,
                Net = model.Net,
                Tax1 = model.Tax1,
                Tax2 = model.Tax2,
                Tax3 = model.Tax3,
                Total = model.Total,
                TimePayment = model.TimePayment,
                WhoPay = model.WhoPay,
                Status = "0"
            };

            await _transactheaderService.CreateAsSync(transactheader);
            return Ok();
        }
        [HttpGet("XacNhanMuaHang/{id}")]
        public async Task<IActionResult> XacNhanMuaHang(string id)
        {
            var transactheader = _transactheaderService.GetById(id);
            if (transactheader != null && transactheader.Status.Equals("0"))
            {
                transactheader.Status = "1";
                await _transactheaderService.UpdateAsSync(transactheader);
                return Ok();
            }
            return BadRequest("Don hang khong tim thay hoặc Status != 0 !");
        }

        [HttpGet("BanGiaoShipper/{id}")]
        public async Task<IActionResult> BanGiaoShipper(string id)
        {
            var transactheader = _transactheaderService.GetById(id);
            if (transactheader != null && transactheader.Status.Equals("1"))
            {
                var transactdetails = _transactdetailService.GetTransactdetailsByTransactheaderId(transactheader.TransactId);
                if (transactdetails != null)
                {
                    foreach (Transactdetail detail in transactdetails)
                    {
                        var product = _productService.GetById(detail.ProductNum);
                        Console.WriteLine("PRODUCT NUM: "+ detail.ProductNum);
                        var sl_conlai = int.Parse(product.Quan) - detail.Quan;
                        if (sl_conlai >= 0)
                        {
                            product.Quan = sl_conlai.ToString();
                        }
                        else
                            return BadRequest("Số lượng sản phẩm " + product.ProductNum + " không đủ");
                        await _productService.UpdateAsSync(product);
                    }
                }
                var user = _useraccountService.GetById(transactheader.WhoPay);
                user.TotalCash = (float.Parse(user.TotalCash) + float.Parse(transactheader.Total)).ToString();

                transactheader.Status = "2";
                await _transactheaderService.UpdateAsSync(transactheader);
                return Ok();
            }
            return BadRequest("Đơn hàng không tìm thấy hoặc chưa xác nhận mua hoặc đã giao cho shipper hoặc đã bị xóa !");
        }

    }
}
