using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Web.Http.Filters;
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
        private IVodkaUserService _vodkaUserService;
        private IProductService _productService;

        private UserManager<VodkaUser> _userManager;
        public TransactheaderController(ITransactdetailService transactdetailService, ITransactheaderService transactheaderService, IProductService productService, IVodkaUserService vodkaUserService)
        {
            _transactheaderService = transactheaderService;
            _productService = productService;
            _transactdetailService = transactdetailService;
            _vodkaUserService = vodkaUserService;
        }

        [HttpGet("GetAllTransactheaders")]
        public IActionResult GetAll()
        {
            var transactheaderList = _transactheaderService.GetAll().Select(x => new TransactheaderIndexViewModel
            {
                TransactHeaderId = x.TransactHeaderId,
                Net = x.Net,
                Tax1 = x.Tax1,
                Tax2 = x.Tax2,
                Tax3 = x.Tax3,
                Total = x.Total,
                TimePayment = x.TimePayment,
                UserId = x.UserId,
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
                TransactHeaderId = transactheader.TransactHeaderId,
                Net = transactheader.Net,
                Tax1 = transactheader.Tax1,
                Tax2 = transactheader.Tax2,
                Tax3 = transactheader.Tax3,
                Total = transactheader.Total,
                TimePayment = transactheader.TimePayment,
                UserId = transactheader.UserId,
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
                                        TransactHeaderId = x.TransactHeaderId,
                                        Net = x.Net,
                                        Tax1 = x.Tax1,
                                        Tax2 = x.Tax2,
                                        Tax3 = x.Tax3,
                                        Total = x.Total,
                                        TimePayment = x.TimePayment,
                                        UserId = x.UserId,
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
            var transactheader = _transactheaderService.GetById(model.TransactHeaderId);
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
            if (model.Total < 0)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
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
                TransactHeaderId = new_str_id,
                Net = model.Net,
                Tax1 = model.Tax1,
                Tax2 = model.Tax2,
                Tax3 = model.Tax3,
                Total = model.Total,
                TimePayment = model.TimePayment,
                UserId = model.UserId,
                Status = 0
            };

            await _transactheaderService.CreateAsSync(transactheader);
            return Ok();
        }
        [HttpGet("XacNhanMuaHang/{id}")]
        [Authorize]
        public async Task<IActionResult> XacNhanMuaHang(string id)
        {
            var transactheader = _transactheaderService.GetById(id);
            if (transactheader != null && transactheader.Status.Equals("0"))
            {
                transactheader.Status = 1;
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
                var transactdetails = _transactdetailService.GetTransactdetailsByTransactheaderId(transactheader.TransactHeaderId);
                if (transactdetails != null)
                {
                    foreach (Transactdetail detail in transactdetails)
                    {
                        var product = _productService.GetById(detail.ProductId);
                        var sl_conlai = product.Quan - detail.Quan;
                        if (sl_conlai >= 0)
                        {
                            product.Quan = sl_conlai;
                        }
                        else
                            return BadRequest("Số lượng sản phẩm " + product.ProductId + " không đủ");
                        await _productService.UpdateAsSync(product);
                    }
                }
                var user = await _userManager.FindByIdAsync(transactheader.UserId);
                user.TotalCash = user.TotalCash + transactheader.Total;

                await _userManager.UpdateAsync(user);

                transactheader.Status = 2;
                await _transactheaderService.UpdateAsSync(transactheader);
                return Ok();
            }
            return BadRequest("Đơn hàng không tìm thấy hoặc chưa xác nhận mua hoặc đã giao cho shipper hoặc đã bị xóa !");
        }

    }
}
