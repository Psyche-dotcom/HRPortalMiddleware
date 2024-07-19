

using HRPortal.Core.DTO;

namespace HRPortal.Infrastructure.Service.Interface
{
    public interface IAccountService
    {
        Task<ResponseDto<string>> RegisterCompany(SignUp signUp, string Role);

        Task<ResponseDto<LoginResultDto>> LoginCompany(SignInModel signIn);
        Task<ResponseDto<string>> ForgotPassword(string UserEmail);
        Task<ResponseDto<string>> ConfirmEmailAsync(int token, string email);
        Task<ResponseDto<CompanyInfo>> CompanyInfoAsync(string companyid);

        Task<ResponseDto<string>> ResetCompanyPassword(ResetPassword resetPassword);
    }
}
