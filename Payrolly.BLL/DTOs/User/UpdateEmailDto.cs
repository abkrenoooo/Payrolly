﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.User
{
    public class UpdateEmailDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Compare("Email", ErrorMessage = "Not Match Email")]
        public string ConfirmedEmail { get; set; } = null!;
    }
}