

using HRPortal.Core.Repository.Implementation;
using HRPortal.Core.Repository.Interface;
using HRPortal.HRPortalProfile;
using HRPortal.Infrastructure.Service.Implementation;
using HRPortal.Infrastructure.Service.Interface;
using HRPortal.Repository.Implementation;



namespace HRPortal.Extension
{
    public static class RegisteredServices
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped(typeof(IHRPortalRepository<>), typeof(HRPortalRepository<>));
            services.AddScoped<IGenerateJwt, GenerateJwt>();
            services.AddScoped<IEmailServices, EmailService>();
            services.AddScoped<IStripePaymentService, StripePaymentService>();
            services.AddScoped<IPaymentRepo, PaymentRepo>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddAutoMapper(typeof(HRPortalProjectProfile));

        }
    }
}
