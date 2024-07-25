using HRPortal.Core.DTO;
using HRPortal.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace HRPortal.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/company")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        
        [HttpPost("staff/create")]
        public async Task<IActionResult> createStaff(AddStaffDto staff)
        {
            var companyId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            if (companyId == null)
            {
                return BadRequest(new ResponseDto<string>()
                {
                    ErrorMessages = new List<string>() { "Invalid user" },
                    DisplayMessage = "Error",
                    StatusCode = 400,

                });
            }
           
            var result = await _staffService.AddStaff(staff, companyId);

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
        [HttpPut("staff/info/update")]
        public async Task<IActionResult> UpdateStaffInfo(UpdateStaffDto staff)
        {
            var companyId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            if (companyId == null)
            {
                return BadRequest(new ResponseDto<string>()
                {
                    ErrorMessages = new List<string>() { "Invalid user" },
                    DisplayMessage = "Error",
                    StatusCode = 400,

                });
            }

            var result = await _staffService.UpdateStaffinfo(staff);

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }


        }

        [HttpGet("staff/all")]
        public async Task<IActionResult> GetStaff(int per_page_size, int page_number)
        {
            var companyId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            if (companyId == null)
            {
                return BadRequest(new ResponseDto<string>()
                {
                    ErrorMessages = new List<string>() { "Invalid user" },
                    DisplayMessage = "Error",
                    StatusCode = 400,

                });
            }

            var result = await _staffService.GetAllStaff(page_number, per_page_size,companyId);

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
        [HttpGet("staff/single")]
        public async Task<IActionResult> GetSingleStaff(string staffid)
        {
            var companyId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            if (companyId == null)
            {
                return BadRequest(new ResponseDto<string>()
                {
                    ErrorMessages = new List<string>() { "Invalid user" },
                    DisplayMessage = "Error",
                    StatusCode = 400,

                });
            }

            var result = await _staffService.GetSingleStaff(staffid,companyId);

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
        [HttpDelete("staff/delete")]
        public async Task<IActionResult> DeleteStaff(string staffid)
        {
            var result = await _staffService.DeleteStaff(staffid);

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
    }
}
