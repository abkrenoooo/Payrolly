using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Payrolly.DAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Employee
{
    public class UpdatePayTypeDto
    {
        public string Id { get; set; } = null!;

        public PayTypes? PayType { get; set; }

        public PayFrequency? PayFrequency { get; set; }

        public decimal Salary { get; set; }

        public double HoursPerDay { get; set; }

        public int DaysPerWeek { get; set; }
    }
}
