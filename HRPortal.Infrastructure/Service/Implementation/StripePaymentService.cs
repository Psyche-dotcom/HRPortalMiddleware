
using HRPortal.Core.DTO;
using HRPortal.Core.Entities;
using HRPortal.Core.Repository.Interface;
using HRPortal.Enitities;
using HRPortal.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;

namespace HRPortal.Infrastructure.Service.Implementation
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly IPaymentRepo _paymentdb;
        private readonly IHRPortalRepository<CompanySubscription> _subscriptionRepo;
        private readonly IConfiguration _configuration;
        private readonly ILogger<StripePaymentService> _logger;
        private readonly IAccountRepo _accountRepo;
        public StripePaymentService(IConfiguration configuration,
            IPaymentRepo paymentdb,
            ILogger<StripePaymentService> logger,
            IAccountRepo accountRepo,
            IHRPortalRepository<CompanySubscription> subscriptionRepo)
        {
            _configuration = configuration;
            _paymentdb = paymentdb;
            _logger = logger;
            _accountRepo = accountRepo;
            _subscriptionRepo = subscriptionRepo;
        }

        public async Task<ResponseDto<string>> IntializeStripepayment(int validity, string companyid)
        {

            long amount = 50 * validity;
            var description = $"Subscription order purchase for {amount} for {validity} months$";
            StripeConfiguration.ApiKey = _configuration["StripeKey:SecretKey"];
            var stripePayment = new Payments();
            var response = new ResponseDto<string>();
            try
            {
                var options = new SessionCreateOptions
                {
                    SuccessUrl = _configuration["StripeKey:redirect_url"]+
                    $"?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = _configuration["StripeKey:redirect_url"],
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment"
                };

                var sessionListItem = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = amount * 100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = description
                        }
                    },
                    Quantity = 1
                };

                options.LineItems.Add(sessionListItem);

                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                if (session.Status != "open")
                {
                    response.ErrorMessages = new List<string>() { "Failed to make order for subscription" };
                    response.DisplayMessage = $"Error";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                stripePayment.PaymentChannel = "STRIPE";
                stripePayment.OrderReferenceId = session.Id;
                stripePayment.SubscriptionValidity = validity;
                stripePayment.Amount = amount.ToString("0.00");
                stripePayment.IsActive = true;
                stripePayment.Description = description;
                stripePayment.CompanyId = companyid;
                var addpaymenttoDb = await _paymentdb.AddPayments(stripePayment);
                if (addpaymenttoDb == false)
                {
                    response.ErrorMessages = new List<string>() { "Failed to make order for subscription" };
                    response.DisplayMessage = $"Error";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;

                }
                response.Result = session.Url;
                response.StatusCode = 200;
                response.DisplayMessage = "Successfully created stripe payment";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Failed to make order for subscription" };
                response.DisplayMessage = $"Error";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }
        }
        public async Task<ResponseDto<string>> confirmStripepayment(string session_id)
        {
            var response = new ResponseDto<string>();
            StripeConfiguration.ApiKey = _configuration["StripeKey:SecretKey"];
            try
            {
                var retrievePayment = await _paymentdb.GetPaymentById(session_id);
                if (retrievePayment == null)
                {
                    response.ErrorMessages = new List<string>() {
                        "Invalid order" };
                    response.DisplayMessage = $"Error";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                var service = new SessionService();
                var session = await service.GetAsync(session_id);
                if (retrievePayment.IsActive == false)
                {
                    response.ErrorMessages = new List<string> { "Invalid Transaction" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                if (session.PaymentStatus == "paid")
                {
                    retrievePayment.IsActive = false;
                    retrievePayment.PaymentStatus = session.PaymentStatus;
                    retrievePayment.CompletePaymentTime = DateTime.UtcNow;
                    var updatetransaction = await _paymentdb.UpdatePayments(retrievePayment);
                    if (!updatetransaction)
                    {
                        _logger.LogError("Error update payment", "Error in updating payment transaction");
                        response.ErrorMessages = new List<string> { "Error in completing transaction" };
                        response.DisplayMessage = "Error";
                        response.StatusCode = StatusCodes.Status501NotImplemented;
                        return response;
                    }
                    var subscription = await _subscriptionRepo.GetQueryable().FirstOrDefaultAsync(u => u.CompanyId == retrievePayment.CompanyId);
                    if (subscription == null)
                    {
                        var subscriptionComp = new CompanySubscription
                        {

                            CompanyId = retrievePayment.CompanyId,
                            SubscrptionStart = DateTime.UtcNow,
                            SubscrptionEnd = DateTime.UtcNow.AddMonths(retrievePayment.SubscriptionValidity)
                        };
                        await _subscriptionRepo.Add(subscriptionComp);
                        await _subscriptionRepo.SaveChanges();
                    }
                    else
                    {

                        if (subscription.SubscrptionEnd > DateTime.UtcNow)
                        {
                            // Subscription is still active, extend the end date
                            subscription.SubscrptionEnd = subscription.SubscrptionEnd.AddMonths(retrievePayment.SubscriptionValidity);
                        }
                        else
                        {
                            // Subscription has ended, reset the start and end dates
                            subscription.SubscrptionStart = DateTime.UtcNow;
                            subscription.SubscrptionEnd = DateTime.Now.AddMonths(retrievePayment.SubscriptionValidity);
                        }
                        _subscriptionRepo.Update(subscription);
                        await _subscriptionRepo.SaveChanges();

                    }


                    var updateUser = await _accountRepo.FindUserByIdAsync(retrievePayment.CompanyId);
                    if (updateUser == null)
                    {
                        response.StatusCode = 400;
                        response.DisplayMessage = "Error";
                        response.ErrorMessages = new List<string>() { "Invalid user" };
                        return response;
                    }
                    updateUser.isSubscribed = true;
                    await _accountRepo.UpdateUserInfo(updateUser);

                    response.StatusCode = StatusCodes.Status200OK;
                    response.DisplayMessage = "Successful";
                    response.Result = "Payment Successfully completed";
                    return response;

                }
                retrievePayment.CompletePaymentTime = DateTime.UtcNow;
                retrievePayment.IsActive = false;
                retrievePayment.PaymentStatus = session.PaymentStatus;
                await _paymentdb.UpdatePayments(retrievePayment);
                response.ErrorMessages = new List<string> { "Invalid Transaction" };
                response.DisplayMessage = "Error";
                response.StatusCode = StatusCodes.Status400BadRequest;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in validating transaction" };
                response.DisplayMessage = $"Error";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }




        }
    }
}
