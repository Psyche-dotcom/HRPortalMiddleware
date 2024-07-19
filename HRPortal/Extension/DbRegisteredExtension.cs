﻿
using HRPortal.Core.Context;
using HRPortal.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace HRPortal.Extension
{
    public static class DbRegisteredExtension
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationCompany, IdentityRole>()
                    .AddEntityFrameworkStores<HRPortalContext>()
                    .AddDefaultTokenProviders();

            services.AddDbContext<HRPortalContext>(dbContextOptions =>
            {
                var connectionString = configuration["ConnectionStrings:ProdDb"];
                var maxRetryCount = 3;
                var maxRetryDelay = TimeSpan.FromSeconds(10);

                dbContextOptions.UseNpgsql(connectionString, options =>
                {
                    options.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null);

                });
            });
        }
    }
}
