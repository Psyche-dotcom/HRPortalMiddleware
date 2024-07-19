


using HRPortal.Core.DTO;
using HRPortal.Core.Entities;
using HRPortal.Core.Repository.Implementation;
using HRPortal.Core.Repository.Interface;
using HRPortal.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HRPortal.Infrastructure.Service.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IHRPortalRepository<CompanySubscription> _subscriptionRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly IHRPortalRepository<ForgetPasswordToken> _forgetPasswordTokenRepo;
        private readonly ILogger<AccountService> _logger;
        private readonly IGenerateJwt _generateJwt;
        private readonly IEmailServices _emailServices;
        public AccountService(IAccountRepo accountRepo,
            ILogger<AccountService> logger,
            IEmailServices emailServices,
            IGenerateJwt generateJwt,
            IHRPortalRepository<ForgetPasswordToken> forgetPasswordTokenRepo,
            IHRPortalRepository<CompanySubscription> subscriptionRepo)
        {
            _accountRepo = accountRepo;
            _logger = logger;
            _generateJwt = generateJwt;

            _emailServices = emailServices;
            _forgetPasswordTokenRepo = forgetPasswordTokenRepo;
            _subscriptionRepo = subscriptionRepo;
        }
        public async Task<ResponseDto<string>> RegisterCompany(SignUp signUp, string Role)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkUserExist = await _accountRepo.FindUserByEmailAsync(signUp.Email);
                if (checkUserExist != null)
                {
                    response.ErrorMessages = new List<string>() { "Company with the email already exist" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkRole = await _accountRepo.RoleExist(Role);
                if (checkRole == false)
                {
                    response.ErrorMessages = new List<string>() { "Role is not available" };
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var mapAccount = new ApplicationCompany();
                mapAccount.Email = signUp.Email;
                mapAccount.PhoneNumber = signUp.PhoneNumber;
                mapAccount.UserName = signUp.Email;
                mapAccount.Location = signUp.Location;
                mapAccount.CompanyName = signUp.CompanyName;
              
                var createUser = await _accountRepo.SignUpAsync(mapAccount, signUp.Password);
                if (createUser == null)
                {
                    response.ErrorMessages = new List<string>() { "Company not created successfully" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var addRole = await _accountRepo.AddRoleAsync(createUser, Role);
                if (addRole == false)
                {
                    response.ErrorMessages = new List<string>() { "Fail to add role to company" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var GenerateConfirmEmailToken = new ConfirmEmailToken()
                {
                    Token = _accountRepo.GenerateConfirmEmailToken(),
                    UserId = createUser.Id
                };
                var Generatetoken = await _accountRepo.SaveGenerateConfirmEmailToken(GenerateConfirmEmailToken);
                if( Generatetoken == null )
                {
                    response.ErrorMessages = new List<string>() { "Fail to generate confirm email token for company" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var message = new Message(new string[] { createUser.Email}, "Confirm Email Token", $"<p>Your confirm email code is below<p><h6>{GenerateConfirmEmailToken.Token}</h6>");
                _emailServices.SendEmail(message);
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "Company successfully created";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in resgistering the company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<LoginResultDto>> LoginCompany(SignInModel signIn)
        {
            var response = new ResponseDto<LoginResultDto>();
            try
            {
                var checkUserExist = await _accountRepo.FindUserByEmailAsync(signIn.Email);
                if (checkUserExist == null)
                {
                    response.ErrorMessages = new List<string>() { "There is no company with the email provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (checkUserExist.isSuspended == true)
                {
                    response.ErrorMessages = new List<string>() { "Company is suspended, contact admin" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkPassword = await _accountRepo.CheckAccountPassword(checkUserExist, signIn.Password);
                if (checkPassword == false)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Password" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var generateToken = await _generateJwt.GenerateToken(checkUserExist);
                if (generateToken == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in generating jwt for company" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
             
                var getUserRole = await _accountRepo.GetUserRoles(checkUserExist);
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successfully login";
                response.Result = new LoginResultDto() { Jwt = generateToken, UserRole = getUserRole };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in login the company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> ForgotPassword(string CompanyEmail)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkCompany = await _accountRepo.FindUserByEmailAsync(CompanyEmail);
                if (checkCompany == null)
                {
                    response.ErrorMessages = new List<string>() { "Email is not available" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var result = await _accountRepo.ForgotPassword(checkCompany);
                if (result == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in generating reset token for company" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var retrieveToken = await _forgetPasswordTokenRepo.GetQueryable().FirstOrDefaultAsync(u => u.companyid == checkCompany.Id);
                if (retrieveToken != null)
                {
                    _forgetPasswordTokenRepo.Delete(retrieveToken);
                    await _forgetPasswordTokenRepo.SaveChanges();
                }
                var generateToken = _accountRepo.GenerateToken();
                var savetoken = await _forgetPasswordTokenRepo.Add(new ForgetPasswordToken()
                {
                    token = generateToken.ToString(),
                    gentoken = result,
                    companyid = checkCompany.Id
                });
                await _forgetPasswordTokenRepo.SaveChanges();
                var message = new Message(new string[] { checkCompany.Email }, "Reset Password Code", $"<p>Your reset password code is below<p><br/><h6>{generateToken}</h6><br/> <p>Please use it in your reset password page</p>");
                _emailServices.SendEmail(message);
                response.DisplayMessage = "Token generated Successfully";
                response.Result = generateToken.ToString();
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in generating reset token for company" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<CompanyInfo>> CompanyInfoAsync(string companyid)
        {
            var response = new ResponseDto<CompanyInfo>();
            try
            {
                var fetchCompany = await _accountRepo.FindUserByIdAsync(companyid);
                if (fetchCompany == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid company" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var result = new CompanyInfo()
                {
                    Id = fetchCompany.Id,
                    CompanyEmail = fetchCompany.Email,
                    CompanyName = fetchCompany.CompanyName,
                    isSubscribed = fetchCompany.isSubscribed,
                    isSuspended = fetchCompany.isSuspended,
                    Location = fetchCompany.Location

                };
                var subscription = await _subscriptionRepo.GetQueryable().FirstOrDefaultAsync(u => u.CompanyId == fetchCompany.Id);
              
                
                if (subscription != null)
                {
                    result.SubscrptionEnd = subscription.SubscrptionEnd;
                    result.SubscrptionStart = subscription.SubscrptionStart;
                }

                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting company info" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<string>> ResetCompanyPassword(ResetPassword resetPassword)
        {
            var response = new ResponseDto<string>();
            try
            {
                var findCompany = await _accountRepo.FindUserByEmailAsync(resetPassword.Email);
                if (findCompany == null)
                {
                    response.ErrorMessages = new List<string>() { "There is no company with the email provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var retrieveToken = await _forgetPasswordTokenRepo.GetQueryable().FirstOrDefaultAsync(u => u.companyid == findCompany.Id);
                if (retrieveToken == null)
                {
                    response.ErrorMessages = new List<string>() { "invalid company token" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                resetPassword.Token = retrieveToken.gentoken;
                var resetPasswordAsync = await _accountRepo.ResetPasswordAsync(findCompany, resetPassword);
                if (resetPasswordAsync == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                _forgetPasswordTokenRepo.Delete(retrieveToken);
                await _forgetPasswordTokenRepo.SaveChanges();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = "Successfully reset company password";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in reset company password" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> ConfirmEmailAsync(int token, string email)
        {
            var response = new ResponseDto<string>();
            try
            {
                var findUser = await _accountRepo.FindUserByEmailAsync(email);
                if (findUser == null)
                {
                    response.ErrorMessages = new List<string>() { "There is no company with the email provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var retrieveToken = await _accountRepo.retrieveUserToken(findUser.Id);
                if(retrieveToken == null)
                {
                    response.ErrorMessages = new List<string>() { "Error company token token" };
                    response.DisplayMessage ="Error";
                    response.StatusCode = 400;
                    return response;
                }
                if(retrieveToken.Token != token)
                {
                    response.ErrorMessages = new List<string>() { "Invalid company token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var deleteToken = await _accountRepo.DeleteUserToken(retrieveToken);
                if(deleteToken == false)
                {
                    response.ErrorMessages = new List<string>() { "Error removing company token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                findUser.EmailConfirmed = true;
                var updateUserConfirmState = await _accountRepo.UpdateUserInfo(findUser);
                if(updateUserConfirmState == false)
                {
                    response.ErrorMessages = new List<string>() { "Error in confirming company token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = "Successfully comfirm company token";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in confirming company token" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }

    }
}
