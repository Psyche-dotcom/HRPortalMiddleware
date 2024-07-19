using HRPortal.Core.Context;
using HRPortal.Core.DTO;
using HRPortal.Core.Entities;
using HRPortal.Core.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRPortal.Core.Repository.Implementation
{
    public class AccountRepo : IAccountRepo
    {

        private readonly UserManager<ApplicationCompany> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HRPortalContext _context;

        public AccountRepo(UserManager<ApplicationCompany> userManager, RoleManager<IdentityRole> roleManager, HRPortalContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<bool> AddRoleAsync(ApplicationCompany user, string Role)
        {
            var AddRole = await _userManager.AddToRoleAsync(user, Role);
            if (AddRole.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveRoleAsync(ApplicationCompany user, IList<string> role)
        {
            var removeRole = await _userManager.RemoveFromRolesAsync(user, role);
            if (removeRole.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<IList<string>> GetUserRoles(ApplicationCompany user)
        {
            var getRoles = await _userManager.GetRolesAsync(user);
            if (getRoles != null)
            {
                return getRoles;
            }
            return null;
        }

        public async Task<bool> RoleExist(string Role)
        {
            var check = await _roleManager.RoleExistsAsync(Role);
            return check;
        }
        public async Task<bool> ConfirmEmail(string token, ApplicationCompany user)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserByEmail(ApplicationCompany user)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<ApplicationCompany?> FindUserByEmailAsync(string email)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser == null)
            {
                return null;
            }
            return findUser;
        }

        public async Task<ApplicationCompany> FindUserByIdAsync(string id)
        {
            var findUser = await _userManager.FindByIdAsync(id);
            return findUser;
        }

        public async Task<string> ForgotPassword(ApplicationCompany user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }
        public async Task<bool> CheckEmailConfirmed(ApplicationCompany user)
        {
            var checkConfirm = user.EmailConfirmed == true;
            return checkConfirm;
        }

        public async Task<bool> CheckAccountPassword(ApplicationCompany user, string password)
        {
            var checkUserPassword = await _userManager.CheckPasswordAsync(user, password);
            return checkUserPassword;
        }

        public async Task<ResetPassword> ResetPasswordAsync(ApplicationCompany user, ResetPassword resetPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (result.Succeeded)
            {
                return resetPassword;
            }
            return null;
        }

        public async Task<ApplicationCompany> SignUpAsync(ApplicationCompany user, string Password)
        {
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }

        public async Task<bool> UpdateUserInfo(ApplicationCompany applicationUser)
        {
            var updateUserInfo = await _userManager.UpdateAsync(applicationUser);
            if (updateUserInfo.Succeeded)
            {
                return true;
            }
            return false;
        }

        public int GenerateConfirmEmailToken()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 1000000);
            return randomNumber;
        }
        public async Task<ConfirmEmailToken> SaveGenerateConfirmEmailToken(ConfirmEmailToken emailToken)
        {
            var saveToken = await _context.ConfirmEmailTokens.AddAsync(emailToken);
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return emailToken;
            }
            return null;
        }
        public int GenerateToken()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 1000000);
            return randomNumber;
        }
        public async Task<ConfirmEmailToken> retrieveUserToken(string userid)
        {
            return await _context.ConfirmEmailTokens.FirstOrDefaultAsync(u => u.UserId == userid);
        }
        public async Task<bool> DeleteUserToken(ConfirmEmailToken token)
        {
            _context.ConfirmEmailTokens.Remove(token);
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return true;
            }
            return false;
        }
    }
}
