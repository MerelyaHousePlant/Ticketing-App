using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketing_App.Dtos.User;

namespace Ticketing_App.Services.UserServices
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDto>> GetUsersById(int id);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser);
    }
}