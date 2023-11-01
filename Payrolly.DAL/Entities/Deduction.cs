﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Entities
{
    public class Deduction
    {
        [Key]
        public string Id { get; set; } = null!;

        public string? DeductionType { get; set; }

        public string? Type { get; set; } = null!;

        public string? CalculatedAs { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AnnualMaximum { get; set; }

        public string? Description { get; set; }
        //Nevegation Property
        public List<Employee>? Employees { get; set; }
    }
}
