using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.DTOs.PaySchedul;
using Payrolly.DAL.Constants;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Mapping
{
    public static class PayScheduleMapping
    {

        #region FromPaySchedulDtotoPayScheduleEntity
        public static PaySchedule FromPaySchedulDtotoPayScheduleEntity(this PayScheduleDto dto)
            => new()
            {
                PayScheduleId = Guid.NewGuid().ToString(),
                Name = dto.Name,
                PayFrequencyTypes = dto.PayFrequencyTypes,
                NextPayDay = dto.NextPayDay,
                NextPayPeriod = dto.NextPayPeriod,
                CompanyId = dto.CompanyId,
            };
        #endregion

        #region To PaySchedule DTO
        public static PayScheduleDto ToPayScheduleDto(this PaySchedule paySchedule)
        {
            return new()
            {
                PayScheduleId= paySchedule.PayScheduleId,   
                Name = paySchedule.Name,
                PayFrequencyTypes = paySchedule.PayFrequencyTypes,
                PayFrequencyTypesName = paySchedule.PayFrequencyTypes==null?null : paySchedule.PayFrequencyTypes.GetType().GetMember(paySchedule.PayFrequencyTypes.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name,
                NextPayPeriod = paySchedule.NextPayPeriod,
                NextPayDay = paySchedule.NextPayDay,
                CompanyId = paySchedule.CompanyId,
                
            };
        }
        #endregion

    }
}
