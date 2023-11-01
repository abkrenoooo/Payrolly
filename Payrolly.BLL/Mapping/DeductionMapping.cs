using Payrolly.BLL.DTOs.Deduction;
using Payrolly.BLL.DTOs.Employee;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Mapping
{
    public static class DeductionMapping
    {
        #region FromCreateDeductionToDeductionEntity
        public static Deduction FromCreateDeductionToDeductionEntity(this DeductionDto dto)
            => new()
            {
                Id = Guid.NewGuid().ToString(),
                DeductionType=dto.DeductionType,
                Type=dto.Type,
                CalculatedAs = dto.CalculatedAs,
                Amount = dto.Amount,
                AnnualMaximum = dto.AnnualMaximum,
                Description = dto.Description
            };
        #endregion

        #region ToDeductionDto
        public static DeductionDto ToDeductionDto(this Deduction employee)
        {
            return new()
            {
                Id = employee.Id,
                DeductionType = employee.DeductionType,
                Type = employee.Type,
                CalculatedAs = employee.CalculatedAs,
                Amount = employee.Amount,
                AnnualMaximum = employee.AnnualMaximum,
                Description = employee.Description
            };
        }
        #endregion

        #region ToDeductionEntity
        public static Deduction ToDeductionEntity(this DeductionDto employee)
        {
            return new()
            {
                Id = employee.Id,
                DeductionType = employee.DeductionType,
                Type = employee.Type,
                CalculatedAs = employee.CalculatedAs,
                Amount = employee.Amount,
                AnnualMaximum = employee.AnnualMaximum,
                Description = employee.Description
            };
        }
        #endregion

    }
}
