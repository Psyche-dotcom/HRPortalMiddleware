
using HRPortal.Core.DTO;
using HRPortal.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace HRPortal.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IStripePaymentService _stripePayment;

        public PaymentController(IStripePaymentService stripePayment)
        {
            _stripePayment = stripePayment;
        }


        [HttpPost("create/stripe")]
        public async Task<IActionResult> createStripePayment(string validity)
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

            var result = await _stripePayment.IntializeStripepayment(int.Parse(validity), companyId);

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

        [HttpPost("webhook/confirm-payment/stripe")]
        public async Task<IActionResult> ConfirmStripePayment(string session_id)
        {
            var result = await _stripePayment.confirmStripepayment(session_id);

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
