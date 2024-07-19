

using HRPortal.Core.DTO;

namespace HRPortal.Infrastructure.Service.Interface
{
    public interface IStripePaymentService
    {
        Task<ResponseDto<string>> IntializeStripepayment(int validity, string companyid);
        Task<ResponseDto<string>> confirmStripepayment(string session_id);
    }
}
