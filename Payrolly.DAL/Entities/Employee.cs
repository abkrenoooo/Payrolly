using Payrolly.DAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Entities
{
    public class Employee
    {
        #region Employee Columns
        [Key] 
        public string Id { get; set; } = null!;

        public string? Ssn { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MaxLength(15)]
        public string? MiddleInitial { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required, MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }
       
        public string? Gender { get; set; }

        public string? ImageUrl { get; set; }

        public string? Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }

        public string? JobTitle { get; set; }

        public string? PaymentMethod { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }

        #endregion

        #region Employee Address Columns
        public string? Country { get; set; }

        public string? Address { get; set; }

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZIPCode { get; set; }
        #endregion

        #region Pay Type

        public PayTypes? PayType { get; set; }

        public PayFrequency? PayFrequency { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }

        public double HoursPerDay { get; set; }

        public int DaysPerWeek { get; set; }

        #endregion

        #region Navegation  Property

        public string? PayScheduleId { get; set; }

        [ForeignKey("PayScheduleId")]
        public virtual PaySchedule? PaySchedule { get; set; }


        public string? LocationId { get; set; }

        [ForeignKey("LocationId")]
        public  virtual Location? Location { get; set; }


        public virtual List<Deduction>? Deductions { get; set; }

        public virtual List<GrossPay>? GrossPay { get; set; } 
        [ForeignKey(nameof(TaxWithholding))]
        public string? TaxWithholdingId { get; set; }
        public virtual TaxWithholding? TaxWithholding { get; set; }
        #endregion
    }
}
