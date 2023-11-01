using Microsoft.Extensions.Logging;
using Payrolly.BLL.DTOs.Deduction;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Mapping;
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
    public class DeductionService : IDeductionService
    {
        #region Private Properties
        private readonly IDeductionRepository _deductionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<DeductionService> _logger;
        #endregion

        #region Constructor
        public DeductionService(IDeductionRepository deductionRepository, IEmployeeRepository employeeRepository, ILogger<DeductionService> logger)
        {
            _deductionRepository = deductionRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
          
        }
        #endregion

        #region Create
        public async Task<GenericResponse<DeductionDto>> CreateDeductionAsync(DeductionDto dto)
        {
            try
            {
                // check if employee is exist or not
                var employee = await _deductionRepository.FindAsync(e => e.Id == dto.Id);

                if (employee != null)
                    return new() { Message = "the employee is exist already" };

                // prepare EmployeeDeduction entity to create 
                employee = dto.FromCreateDeductionToDeductionEntity();

                // assign employee for default location
              

                if (!await _deductionRepository.CreateAsync(employee))
                    return new() { Message = "cannot create" };

                // return response
                var employeeDto = employee.ToDeductionDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "created",
                    Data = employeeDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
            
        }

        public async Task<GenericResponse<DeductionDto>> CreateEmployeeDeductionAsync(DeductionDto dto, string? employeeId = null)
        {
            try
            {
                // check if employee is exist or not 
                var employee = await _employeeRepository.FindAsync(e => e.Id == employeeId); 

                if (employee != null)
                    return new() { Message = "the employee is exist already" };

                // prepare EmployeeDeduction entity to create 


                // assign employee 

                var newDeduction = dto.FromCreateDeductionToDeductionEntity();

               // var employees = employee.Deductions.Add(newDeduction);

                if (!await _deductionRepository.CreateAsync(newDeduction))
                    return new() { Message = "cannot create" };

                // return response
                var employeeDto = newDeduction.ToDeductionDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "created",
                    Data = employeeDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
            //throw new NotImplementedException();
        }
        #endregion

        #region DeleteDeductionAsync
        public async Task<GenericResponse<object>> DeleteDeductionAsync(string id)
        {
            try
            {
                // check if employee is exist or not
                var employee = await _deductionRepository.FindAsync(e => e.Id == id);

                //can not Delete employee

                if (employee == null)
                    return new() { Message = "cannot Delete" };

                // Delete employee
                if (!await _deductionRepository.RemoveAsync(employee))
                    return new() { Message = "Delete" };

                // return response
                var employeeDto = employee.ToDeductionDto();

                return new()
                {
                    Success = true,
                    StatusCode = 201,
                    Message = "Deleted",
                    Data = employeeDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                // return response
                return new()
                {
                    Message = "Data Not Exist"
                };
            }
           
        }
        #endregion

        #region GetDeductionAsync
        public async Task<GenericResponse<DeductionDto>> GetDeductionAsync(string id)
        {
            try
            {
                // Get By Employee With id
                var employee = await _deductionRepository.GetByIDAsync(id);

                if (employee == null)
                {
                    return new() { Message = "the employee is not exist" };
                }

                // return response
                var employeeDto = employee.ToDeductionDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Data Found",
                    Data = employeeDto

                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                // return response
                return new()
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Not Found"

                };
            }
            
        }
        #endregion
    }
}
