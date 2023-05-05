using Microsoft.AspNetCore.Mvc;
using Vodka.Models.Useraccount;
using VodkaEntities;
using VodkaServices;

namespace Vodka.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UseraccountController : ControllerBase
    {
        private IUseraccountService _useraccountService;
        public UseraccountController(IUseraccountService useraccountService)
        {
            _useraccountService = useraccountService;
        }

        [HttpGet("GetAllUseraccounts")]
        public IActionResult GetAll()
        {
            var useraccountList = _useraccountService.GetAll().Select(x => new UseraccountIndexViewModel
            {
                UserId = x.UserId,
                UserName = x.UserName,
                Password = x.Password,
                AccessLevel = x.AccessLevel,
                TotalCash = x.TotalCash,
                IsActive = x.IsActive,
                Email = x.Email,
                Address = x.Address
            }).ToList();
            return Ok(useraccountList);
        }
        [HttpGet("GetUseraccountById/{id}")]
        public IActionResult GetUseraccountById(string id)
        {
            var useraccount = _useraccountService.GetById(id);
            if (useraccount == null)
                return NotFound();
            var model = new UseraccountDetailViewModel
            {
                UserId = useraccount.UserId,
                UserName = useraccount.UserName,
                Password = useraccount.Password,
                AccessLevel = useraccount.AccessLevel,
                TotalCash = useraccount.TotalCash,
                IsActive = useraccount.IsActive,
                Email = useraccount.Email,
                Address = useraccount.Address
            };
            return Ok(model);
        }
        [HttpDelete("DeleteUseraccountById")]
        public async Task<IActionResult> DeleteUseraccountById(string id)
        {
            try
            {
                await _useraccountService.DeleteById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Id danh muc khong hop le");
            }
            return NoContent();

        }
        [HttpPut("UpdateAsSync")]
        public async Task<IActionResult> UpdateAsSync([FromBody] UseraccountEditViewModel model)
        {
            if (model == null)
                return BadRequest();
            if (model.TotalCash < 0 || model.Password.Trim().Equals(""))
                return BadRequest();

            var useraccount = _useraccountService.GetById(model.UserId);
            if (useraccount == null)
                return NotFound();
            useraccount.Password = model.Password;
            useraccount.AccessLevel = model.AccessLevel;
            useraccount.TotalCash = model.TotalCash;
            useraccount.Address = model.Address;

            await _useraccountService.UpdateAsSync(useraccount);
            return Ok();
        }
        [HttpPost("CreateUseraccount")]
        public async Task<IActionResult> Createuseraccount([FromBody] UseraccountCreateViewModel model)
        {
            if (model == null)
                return BadRequest();
            if (model.TotalCash < 0 || model.Password.Trim().Equals(""))
                return BadRequest();

            string new_str_id = "";
            int new_int_id = _useraccountService.GetLastId() + 1;
            if (new_int_id >= 100 && new_int_id < 1000)
                new_str_id = "U00" + new_int_id.ToString();
            else if (new_int_id < 10 && new_int_id >= 0)
                new_str_id = "U0000" + new_int_id.ToString();
            else if (new_int_id < 100 && new_int_id >= 10)
                new_str_id = "U000" + new_int_id.ToString();

            var useraccount = new Useraccount
            {
                UserId = new_str_id,
                UserName = model.UserName,
                Password = model.Password,
                AccessLevel = model.AccessLevel,
                TotalCash = model.TotalCash,
                Email = model.Email,
                Address = model.Address
            };

            await _useraccountService.CreateAsSync(useraccount);
            return Ok();
        }
    }
}
