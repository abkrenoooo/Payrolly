﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payrolly.DAL.Data;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230712235150_AddTaxWithholding")]
    partial class AddTaxWithholding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DeductionEmployee", b =>
                {
                    b.Property<string>("DeductionsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeesId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DeductionsId", "EmployeesId");

                    b.HasIndex("EmployeesId");

                    b.ToTable("DeductionEmployee");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", "security");

                    b.HasData(
                        new
                        {
                            Id = "98f4df00-a720-4794-bacb-921996ae6d22",
                            ConcurrencyStamp = "294d9109-735e-4481-9754-52c8b4c87ed8",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", "security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", "security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "security");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Occupation")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ZIPCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", "security");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.Company", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FedrialTaxId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("FedrialTaxId")
                        .IsUnique()
                        .HasFilter("[FedrialTaxId] IS NOT NULL");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.Deduction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("AnnualMaximum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CalculatedAs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeductionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Deductions");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DaysPerWeek")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GrossPayId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("HoursPerDay")
                        .HasColumnType("float");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LocationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MiddleInitial")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("PayFrequency")
                        .HasColumnType("int");

                    b.Property<string>("PayScheduleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PayType")
                        .HasColumnType("int");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Ssn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZIPCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GrossPayId");

                    b.HasIndex("LocationId");

                    b.HasIndex("PayScheduleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.FederalTax", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EIN")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("TaxForm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxRate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("FederalTaxes");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.GrossPay", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Bonus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0.0m);

                    b.Property<decimal>("Commision")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0.0m);

                    b.Property<decimal>("EmpTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("HolidayPay")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<double>("OverTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<decimal>("PayRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PayScheduleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("RegularPay")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PayScheduleId");

                    b.ToTable("GrossPays");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.Location", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZIPCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.PaySchedule", b =>
                {
                    b.Property<string>("PayScheduleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NextPayDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NextPayPeriod")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PayFrequencyTypes")
                        .HasColumnType("int");

                    b.Property<bool>("Payment")
                        .HasColumnType("bit");

                    b.HasKey("PayScheduleId");

                    b.HasIndex("CompanyId");

                    b.ToTable("PaySchedules");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.StateTax", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployerAccountNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateTaxType")
                        .HasColumnType("int");

                    b.Property<decimal>("TrainingTaxRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnEmploymentInsuranceRate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("StateTaxes");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.TaxWithholding", b =>
                {
                    b.Property<string>("FederalWithholdingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("AdditionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("CASDI")
                        .HasColumnType("bit");

                    b.Property<bool>("CASUIAndETT")
                        .HasColumnType("bit");

                    b.Property<decimal>("ClaimedDependent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Deducations")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ExtraWithholding")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("FUTA")
                        .HasColumnType("bit");

                    b.Property<bool>("FederalCheck")
                        .HasColumnType("bit");

                    b.Property<int>("FilingStatusFederal")
                        .HasColumnType("int");

                    b.Property<int>("FilingStatusState")
                        .HasColumnType("int");

                    b.Property<decimal>("OtherIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("SocialSecurity")
                        .HasColumnType("bit");

                    b.Property<int>("WithholdingAllowance")
                        .HasColumnType("int");

                    b.HasKey("FederalWithholdingId");

                    b.ToTable("TaxWithholdings");
                });

            modelBuilder.Entity("DeductionEmployee", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.Deduction", null)
                        .WithMany()
                        .HasForeignKey("DeductionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payrolly.DAL.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payrolly.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.ApplicationUser", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.Company", "Company")
                        .WithMany("ApplicationUser")
                        .HasForeignKey("CompanyID");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.Company", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.FederalTax", "FederalTax")
                        .WithOne()
                        .HasForeignKey("Payrolly.DAL.Entities.Company", "FedrialTaxId");

                    b.Navigation("FederalTax");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.Employee", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.GrossPay", "GrossPay")
                        .WithMany()
                        .HasForeignKey("GrossPayId");

                    b.HasOne("Payrolly.DAL.Entities.Location", "Location")
                        .WithMany("Employees")
                        .HasForeignKey("LocationId");

                    b.HasOne("Payrolly.DAL.Entities.PaySchedule", "PaySchedule")
                        .WithMany("Employees")
                        .HasForeignKey("PayScheduleId");

                    b.Navigation("GrossPay");

                    b.Navigation("Location");

                    b.Navigation("PaySchedule");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.FederalTax", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.GrossPay", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.PaySchedule", "PaySchedule")
                        .WithMany("grossPays")
                        .HasForeignKey("PayScheduleId");

                    b.Navigation("PaySchedule");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.Location", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.Company", "Company")
                        .WithMany("Locations")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.PaySchedule", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.Company", "Company")
                        .WithMany("PaySchedules")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.StateTax", b =>
                {
                    b.HasOne("Payrolly.DAL.Entities.Company", "Company")
                        .WithMany("StateTaxes")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.Company", b =>
                {
                    b.Navigation("ApplicationUser");

                    b.Navigation("Locations");

                    b.Navigation("PaySchedules");

                    b.Navigation("StateTaxes");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.Location", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Payrolly.DAL.Entities.PaySchedule", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("grossPays");
                });
#pragma warning restore 612, 618
        }
    }
}
