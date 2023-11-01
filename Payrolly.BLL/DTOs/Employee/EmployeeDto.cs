using Payrolly.DAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Employee
{
    public class EmployeeDto
    {
        #region Employee Columns
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

        public string JobTitle { get; set; }

        public string? PaymentMethod { get; set; }

        #endregion

        #region Employee Address Columns
        public string? Country { get; set; }

        public string? Address { get; set; }

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZIPCode { get; set; }
        #endregion

        #region PayType
        public PayTypes? PayType { get; set; }

        public PayFrequency? PayFrequency { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        
        public double HoursPerDay { get; set; }

        public int DaysPerWeek { get; set; }
        #endregion
    }
}
