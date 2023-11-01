using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.User
{
    public class UpdatePasswordDto
    {
        public string OldPassword { get; set; } = null!;

        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;    

        [Compare(nameof(NewPassword), ErrorMessage = "Not matched password")]
        public string ConfirmedPassword { get; set; } = null!;
    }
}
