using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Employee
{
    public class CreateEmployeeDto
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MaxLength(15)]
        public string? MiddleInitial { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [EmailAddress, MaxLength(50)]
        public string Email { get; set; } = null!;
    }
}
