using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payrolly.DAL.Data;
using Payrolly.DAL.Entities;
using Payrolly.DAL.Interface;
using Payrolly.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL
{
    public static class DALConfiguration
    {
        public static IServiceCollection AddDALConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDBContext(configuration);
            services.AddIdentityConfiguration();
            services.AddRepositoryConfiguration();

            return services;
        }

        private static IServiceCollection AddDBContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        private static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            return services;
        }

        private static IServiceCollection AddRepositoryConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IPayScheduleRepository, PayScheduleRepository>();
            services.AddTransient<IDeductionRepository, DeductionRepository>();
            services.AddScoped<IFederalTaxRepository, FederalTaxRepository>();
            services.AddScoped<IStateTaxRepository, StateTaxRepository>();
            services.AddScoped<IGrossPayRepository, GrossPayRepository>();
            services.AddScoped<ITaxWithholdingRepository, TaxWithholdingRepository>();

            return services;
        }
    }
}
