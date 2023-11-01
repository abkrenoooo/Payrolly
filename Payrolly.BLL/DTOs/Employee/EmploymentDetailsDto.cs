using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Employee
{
    public class EmploymentDetailsDto
    {
        public string Id { get; set; } = null!;

        public string? Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }

        public string? JobTitle { get; set; }

        public string? Location { get; set; }
         
        public string? PaySchedule { get; set; }
    }
}
