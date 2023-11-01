using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Payrolly.BLL.Helpers;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL
{
    public static class BLLConfiguration
    {
        public static IServiceCollection AddBLLConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJWTConfiguration(configuration);
            services.AddBusinessServicesConfiguration();

            return services;
        }

        private static IServiceCollection AddJWTConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWT>(configuration.GetSection("JWT"));

            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddSingleton(tokenValidationParameters);

            return services;
        }

        private static IServiceCollection AddBusinessServicesConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddScoped<IPayScheduleService,PayScheduleService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IDeductionService, DeductionService>();
            services.AddScoped<ITaxService, TaxService>();
            services.AddScoped<IPayCheckService, PayCheckService>();
            services.AddScoped<ITaxWithholdingServices, TaxWithholdingServices>();

            return services;
        }
    }
}
