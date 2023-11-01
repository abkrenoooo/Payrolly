using Payrolly.BLL.DTOs.User;
using Payrolly.BLL.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.IServices
{
    public interface IUserService
    {
        Task<GenericResponse<UserDto>> RegisterUserAsync(RegisterUserDto dto);
        Task<GenericResponse<UserDto>> LoginAsync(LoginDto dto);
        Task<GenericResponse<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<GenericResponse<object>> DeleteUserAsync(string id);
    }
}
