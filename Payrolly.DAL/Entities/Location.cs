using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Payrolly.DAL.Entities
{
    public class Location
    {
        [Key]
        public string Id { get; set; } = null!;

        public string? Country { get; set; }

        public string? Address { get; set; }

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZIPCode { get; set; }

        #region Nevegation Property

        public string? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
        public List<Employee>? Employees { get; set; }

        #endregion

        public override string ToString()
        {
            return String.Format($"{this.State}-{this.City}-{this.ZIPCode}");
        }
    }
}