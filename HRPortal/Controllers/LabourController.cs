using HRPortal.Core.DTO;
using HRPortal.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace HRPortal.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/labour")]
    [ApiController]
    public class LabourController : ControllerBase
    {
        private readonly ILabourService _labourService;

        public LabourController(ILabourService labourService)
        {
            _labourService = labourService;
        }

        [HttpPost("labour/create")]
        public async Task<IActionResult> createLabour(AddLabourDto labour)
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

            var result = await _labourService.AddLabour(labour, companyId);

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
        [HttpPut("labour/info/update")]
        public async Task<IActionResult> UpdateLabourInfo(UpdateLabourDto labour)
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

            var result = await _labourService.UpdateLabourinfo(labour);

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
        [HttpDelete("labour/delete")]
        public async Task<IActionResult> DeleteStaff(string labourid)
        {
            var result = await _labourService.DeleteLabour(labourid);

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
        [HttpGet("labour/all")]
        public async Task<IActionResult> GetALLLabour()
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

            var result = await _labourService.GetAllLabour(companyId);

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

        [HttpGet("labour/single")]
        public async Task<IActionResult> GetSingleLabour(string Labourid)
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

            var result = await _labourService.GetSingleLabour(companyId, Labourid);

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
