using Payrolly.BLL.DTOs.Paycheck;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Mapping
{
    public static class PayCheckMapping
    {
        public static CalculateGrossPayDto CalculateGrossPay(this GrossPay dto)
        {
            return new()
            {
                GrossPayId = dto.Id,
                RegularPay = dto.RegularPay,
                OverTime = dto.OverTime,
                HolidayPay = dto.HolidayPay,
                Bonus = dto.Bonus,
                Commission = dto.Commision,
                EmpTax = dto.EmpTax,
                CheckNum = dto.CheckNum,
                PayRate = dto.PayRate,
                PayScheduleId = dto.PayScheduleId,
                EmployeeId = dto.EmployeeId,
                GrossPayDate = dto.GrossPayDate,
                Payment = dto.Payment,
                CompanyId = dto.CompanyId,
                EmployeeName= dto.Employee==null? null:string.Join(" ", dto.Employee.FirstName, dto.Employee.LastName)
                //TotalHours = dto.RegularPay + dto.OverTime + dto.HolidayPay,
                //Gross = regularSalary + overTimeSalary + holidaySalary + dto.Commission + dto.Bonus
            };
        }
        public static GrossPay ToGrossPay(this CalculateGrossPayDto dto, string CompanyId)
        {
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                RegularPay = dto.RegularPay,
                OverTime = dto.OverTime,
                HolidayPay = dto.HolidayPay,
                Bonus = dto.Bonus,
                Commision = dto.Commission,
                EmpTax = dto.EmpTax,
                PayScheduleId = dto.PayScheduleId,
                PayRate = dto.PayRate,
                CheckNum = dto.CheckNum,
                EmployeeId = dto.EmployeeId,
                Payment = dto.Payment,
                GrossPayDate = dto.GrossPayDate,
                CompanyId = CompanyId
            };
        }
        public static GrossPay ToGrossPayUpdate(this CalculateGrossPayDto dto, string CompanyId)
        {
            return new()
            {
                Id = dto.GrossPayId,
                RegularPay = dto.RegularPay,
                OverTime = dto.OverTime,
                HolidayPay = dto.HolidayPay,
                Bonus = dto.Bonus,
                Commision = dto.Commission,
                EmpTax = dto.EmpTax,
                PayRate = dto.PayRate,
                CheckNum = dto.CheckNum,
                Payment = dto.Payment,
                GrossPayDate = dto.GrossPayDate,
                PayScheduleId = dto.PayScheduleId,
                CompanyId = CompanyId,
                EmployeeId = dto.EmployeeId,

            };
        }
    }
}
