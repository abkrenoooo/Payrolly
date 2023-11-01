using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Employee
{
    public class UpdatePersonalInfoDto
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleInitial { get; set; }
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZIPCode { get; set; }
        public string? Ssn { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
