using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ticketing_App.Data;
using Ticketing_App.Dtos.User;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ticketing_App.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
        .FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var response = new ServiceResponse<List<GetUserDto>>();
            try
            {
            var dbUser = await _context.Users.ToListAsync();
            response.Data = dbUser.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response; 
            }
            if(response.Data!=null)response.Success = true;
            return response; 
        }

        public async Task<ServiceResponse<GetUserDto>> GetUsersById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try
            {
            var dbUser = await _context.Users.FirstOrDefaultAsync(c => c.UserId == id);
            serviceResponse.Data = _mapper.Map<GetUserDto>(dbUser);
            
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
           if(serviceResponse.Data!=null) serviceResponse.Success = true;
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            try
            {
            var user = await _context.Users
            .FirstOrDefaultAsync(c => c.UserId == GetUserId());
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Address = updatedUser.Address;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetUserDto>(user);
           if(response.Data!=null) response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            response.Success = true;
            return response;
        }
    }
}