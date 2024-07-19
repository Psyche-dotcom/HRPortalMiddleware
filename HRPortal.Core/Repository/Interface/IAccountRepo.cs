
using HRPortal.Core.DTO;
using HRPortal.Core.Entities;

namespace HRPortal.Core.Repository.Interface
{
    public interface IAccountRepo
    {

        Task<ApplicationCompany> SignUpAsync(ApplicationCompany user, string Password);

        Task<bool> CheckAccountPassword(ApplicationCompany user, string password);
        int GenerateToken();
        int GenerateConfirmEmailToken();
        Task<bool> DeleteUserToken(ConfirmEmailToken token);
        Task<ConfirmEmailToken> retrieveUserToken(string userid);
        Task<ConfirmEmailToken> SaveGenerateConfirmEmailToken(ConfirmEmailToken emailToken);

        Task<bool> CheckEmailConfirmed(ApplicationCompany user);

        Task<bool> AddRoleAsync(ApplicationCompany user, string Role);

        Task<string> ForgotPassword(ApplicationCompany user);

        Task<bool> ConfirmEmail(string token, ApplicationCompany user);

        Task<bool> RemoveRoleAsync(ApplicationCompany user, IList<string> role);

        Task<ResetPassword> ResetPasswordAsync(ApplicationCompany user, ResetPassword resetPassword);

        Task<bool> RoleExist(string Role);

        Task<ApplicationCompany?> FindUserByEmailAsync(string email);

        Task<ApplicationCompany> FindUserByIdAsync(string id);

        Task<bool> UpdateUserInfo(ApplicationCompany applicationUser);

        Task<IList<string>> GetUserRoles(ApplicationCompany user);

        Task<bool> DeleteUserByEmail(ApplicationCompany user);
    }
}
