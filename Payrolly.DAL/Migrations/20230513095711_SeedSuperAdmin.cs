using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using Payrolly.DAL.Entities;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class SeedSuperAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create the SuperAdmin role if it doesn't already exist
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('3f64cf9c-21a1-4cf7-9e08-78f2b2c85c7a', 'SuperAdmin', 'SUPERADMIN');");

            // Create a new user with the specified details
            var userId = Guid.NewGuid();
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser
            {
                Id = userId.ToString(),
                UserName = "superadmin@superadmin.payrolly",
                NormalizedUserName = "SUPERADMIN@SUPERADMIN.PAYROLLY",
                Email = "superadmin@superadmin.payrolly",
                NormalizedEmail = "SUPERADMIN@SUPERADMIN.PAYROLLY",
                EmailConfirmed = true,
                PhoneNumberConfirmed = false, // Set PhoneNumberConfirmed to false
                TwoFactorEnabled = false, // Set TwoFactorEnabled to false
                AccessFailedCount = 0,
                LockoutEnabled = true,
                PasswordHash = passwordHasher.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] { user.Id, user.UserName, user.NormalizedUserName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.SecurityStamp, user.PhoneNumberConfirmed, user.TwoFactorEnabled, user.LockoutEnabled, user.AccessFailedCount });

            // Add the user to the SuperAdmin role
            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) SELECT '{userId}', '3f64cf9c-21a1-4cf7-9e08-78f2b2c85c7a' WHERE NOT EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = '{userId}' AND RoleId = '3f64cf9c-21a1-4cf7-9e08-78f2b2c85c7a');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the user from the SuperAdmin role
            migrationBuilder.Sql("DELETE FROM AspNetUserRoles WHERE RoleId = '3f64cf9c-21a1-4cf7-9e08-78f2b2c85c7a';");

            // Remove the user from the AspNetUsers table
            migrationBuilder.Sql("DELETE FROM AspNetUsers WHERE UserName = 'superadmin@superadmin.payrolly';");

            // Remove the SuperAdmin role from the AspNetRoles table if it's no longer being used
            migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Id = '3f64cf9c-21a1-4cf7-9e08-78f2b2c85c7a' AND NOT EXISTS (SELECT 1 FROM AspNetUserRoles WHERE RoleId = '3f64cf9c-21a1-4cf7-9e08-78f2b2c85c7a');");
        }
    }
}
