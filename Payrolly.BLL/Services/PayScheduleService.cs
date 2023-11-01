using Microsoft.Extensions.Logging;
using Payrolly.BLL.DTOs.PaySchedul;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Mapping;
using Payrolly.DAL.Constants;
using Payrolly.DAL.Entities;
using Payrolly.DAL.Interface;
using Payrolly.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Services
{
    public class PayScheduleService : IPayScheduleService
    {
        #region Private Properties
        private readonly IPayScheduleRepository _payScheduleRepository;
        private readonly IGrossPayRepository _grossPayRepository;
        private readonly ILogger<PayScheduleService> _logger;
        #endregion

        #region Constructor
        public PayScheduleService(IPayScheduleRepository payScheduleRepository, ILogger<PayScheduleService> logger,IGrossPayRepository grossPayRepository)
        {
            _payScheduleRepository = payScheduleRepository;
            _grossPayRepository = grossPayRepository;
            _logger = logger;
        }
        #endregion

        #region CreatePayScheduleAsync
        public async Task<GenericResponse<PayScheduleDto>> CreatePayScheduleAsync(PayScheduleDto dto, string companyId)
        {
            try
            {
                // check if PaySchedule is exist or not 
                var paySchedule = await _payScheduleRepository.FindAsync(p => p.Name == dto.Name);

                if (paySchedule != null)
                    return new()
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "this name of pay schedule is exist",
                        Data = null
                    };

                // prepare employee entity to create
                paySchedule = dto.FromPaySchedulDtotoPayScheduleEntity();

                paySchedule.Name = dto.Name;
                paySchedule.PayFrequencyTypes = dto.PayFrequencyTypes;
                paySchedule.NextPayPeriod = dto.NextPayPeriod;
                paySchedule.NextPayDay = dto.NextPayDay;
                paySchedule.CompanyId = companyId;

                if (!await _payScheduleRepository.CreateAsync(paySchedule))
                    return new() { Message = "cannot create" };

                // return response
                var payScheduleDto = paySchedule.ToPayScheduleDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "created",
                    Data = payScheduleDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
        }
        #endregion

        #region GetPayScheduleAsync
        public async Task<GenericResponse<PayScheduleDto>> GetPayScheduleAsync(string name)
        {
            try
            {
                // check if PaySchedule is exist or not 
                var paySchedule = await _payScheduleRepository.FindAsync(p => p.Name.Contains(name));

                if (paySchedule == null)
                {
                    return new() { Message = "the paySchedule is not exist" };
                }

                // return response
                var payScheduleDto = paySchedule.ToPayScheduleDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Data Found",
                    Data = payScheduleDto

                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }

        }
        #endregion

        public async Task<GenericResponse<List<PayScheduleDto>>> GetAllPayschedulesAsync(string companyId)
        {
            try
            {
                // get all pay schedules inside the company
                var paySchedules = await _payScheduleRepository.FindAllAsync(p => p.CompanyId == companyId);

                List<PayScheduleDto> PayScheduleDtos = new List<PayScheduleDto>();
                foreach (var p in paySchedules)
                {
                    PayScheduleDtos.Add(p.ToPayScheduleDto());
                }


                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Data Found",
                    Data = PayScheduleDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);

                return new() { Message = ex.Message };
            }
        }

        public async Task<GenericResponse<List<string>>> GetPayPeriodAsync(string companyId, string PayScheduleId)
        {
            try
            {
                var paySchedule = await _payScheduleRepository.GetByIDAsync(PayScheduleId);
                if (paySchedule == null)
                {
                    // Handle case where paySchedules is not found
                    return new() { Message = "paySchedules not found" };
                }
                List<string> PayScheduleDtos = new List<string>();
                List<GrossPay> grossPays = new List<GrossPay>();

                if (paySchedule.PayFrequencyTypes == PayFrequencyTypes.EveryWeek)
                {
                    var NextPayPeriodAfter = paySchedule.NextPayPeriod.AddMonths(1);
                    var NextPayPeriodBefore = paySchedule.NextPayPeriod.AddMonths(-1);
                    for (DateTime i = NextPayPeriodBefore; i <= NextPayPeriodAfter; i.AddDays(6))
                    {
                        PayScheduleDtos.Add($"{i.ToShortDateString()} to {i.AddDays(6).AddDays(-1).ToShortDateString()}");
                        i = i.AddDays(6);
                    }
                }
                else if (paySchedule.PayFrequencyTypes == PayFrequencyTypes.EveryOtherWeek)
                {
                    var NextPayPeriodAfterMonth = paySchedule.NextPayPeriod.AddMonths(2);
                    var NextPayPeriodBeforeMonth = paySchedule.NextPayPeriod.AddMonths(-2);
                    for (DateTime i = NextPayPeriodBeforeMonth; i <= NextPayPeriodAfterMonth; i.AddDays(13))
                    {
                        PayScheduleDtos.Add($"{i.ToShortDateString()} to {i.AddDays(13).AddDays(-1).ToShortDateString()}");
                        i = i.AddDays(13);
                    }
                }
                else if (paySchedule.PayFrequencyTypes == PayFrequencyTypes.TwiceAMonth)
                {
                    var NextPayPeriodAfterMonth = paySchedule.NextPayPeriod.AddMonths(2);
                    var NextPayPeriodBeforeMonth = paySchedule.NextPayPeriod.AddMonths(-2);
                    for (DateTime i = NextPayPeriodBeforeMonth; i <= NextPayPeriodAfterMonth; i.AddDays(14))
                    {
                        PayScheduleDtos.Add($"{i.ToShortDateString()} to {i.AddDays(14).AddDays(-1).ToShortDateString()}");
                        i = i.AddDays(14);
                    }
                }
                else if (paySchedule.PayFrequencyTypes == PayFrequencyTypes.EveryMonth)
                {
                    var NextPayPeriodAfterMonth = paySchedule.NextPayPeriod.AddMonths(3);
                    var NextPayPeriodBeforeMonth = paySchedule.NextPayPeriod.AddMonths(-3);
                    for (DateTime i = NextPayPeriodBeforeMonth; i <= NextPayPeriodAfterMonth; i.AddMonths(1))
                    {
                        PayScheduleDtos.Add($"{i.ToShortDateString()} to {i.AddMonths(1).AddDays(-1).ToShortDateString()}");
                        i = i.AddMonths(1);
                    }
                }

                //var startPeriod = paySchedule.NextPayPeriod.Date;
                //var endPeriod = startPeriod.AddDays(6);

                //var startPeriodString = startPeriod.ToShortDateString();
                //var endPeriodString = endPeriod.ToShortDateString();

                return new()
                {
                    Success = true,
                    Message = "Success",
                    StatusCode = 200,
                    Data = PayScheduleDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
        }
    }
}

