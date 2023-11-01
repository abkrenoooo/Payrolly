using Microsoft.Extensions.Logging;
using Payrolly.BLL.DTOs.Location;
using Payrolly.BLL.DTOs.PaySchedul;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Mapping;
using Payrolly.DAL.Entities;
using Payrolly.DAL.Interface;
using Payrolly.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Services
{
    public class LocationService : ILocationService
    {
        #region Private Properties
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<LocationService> _logger;
        #endregion

        #region Constructor
        public LocationService(ILocationRepository locationRepository, ILogger<LocationService> logger)
        {
            _locationRepository = locationRepository;
            _logger = logger;
        }
        #endregion

        public async Task<GenericResponse<LocationDto>> CreateLocationAsync(LocationDto dto, string companyId)
        {
            try
            {
                var location = dto.ToLocationEntity(companyId);
                
                if (!await _locationRepository.CreateAsync(location))
                    return new() { Message = "cannot create location" };

                // return response
                var locationDto = location.ToLocationDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Location created successfully",
                    Data = locationDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
          
        }

        public async Task<GenericResponse<IEnumerable<string>>> GetAllLocationsAsync(string companyId)
        {
            try
            {
                // get all locations in the company
                var locations = await _locationRepository.FindAllAsync(l => l.CompanyId == companyId, new string[] { "Company" });

                if (!locations.Any())
                    return new() { StatusCode = 404, Message = "there is no any location in the company" };

                var locationsNames = new List<string>();

                foreach (var l in locations)
                    locationsNames.Add(l.ToString());

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Data = locationsNames
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);

                return null;
            }
        }

        public async Task<GenericResponse<LocationDto>> GetLocationAsync(string id)
        {
            try
            {
                // Get By Employee With id
                var location = await _locationRepository.GetByIDAsync(id);

                if (location == null)
                {
                    return new() { Message = "the location is not exist" };
                }

                // return response
                var locationDto = location.ToLocationDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Data Found",
                    Data = locationDto

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
    }
}
