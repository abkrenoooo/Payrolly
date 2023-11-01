using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Payrolly.DAL.Configuration;
using Payrolly.DAL.Constants;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Payrolly.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override async void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            builder.ApplyConfiguration(new GrossPayConfiguration());

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = RolesId.AdminRoleId,
                Name = RoleTypes.Admin.ToString(),
                NormalizedName = RoleTypes.Admin.ToString().ToUpper(),
                ConcurrencyStamp = RolesId.AdminConcurrencyStamp
            });

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<PaySchedule> PaySchedules { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<StateTax> StateTaxes { get; set; }
        public DbSet<FederalTax> FederalTaxes { get; set; }
        public DbSet<GrossPay> GrossPays { get; set; }
        public DbSet<TaxWithholding> TaxWithholdings { get; set; }
       
    }
}
