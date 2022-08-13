using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ticketing_App.Models;
using Ticketing_App.Services.UserServices;
using Ticketing_App.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Ticketing_App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<GetUserDto>>> Get()
        {
            var response = await _userService.GetAllUsers();
            if (response.Data == null)
            {
                return NotFound(response);
            }            
            return Ok(response);
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<GetUserDto>>> GetSingle(int id)
        {
            var response = await _userService.GetUsersById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }            
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateTicket(UpdateUserDto updatedUser)
        {
            var response = await _userService.UpdateUser(updatedUser);
            if (response.Data == null)
            {
                return NotFound(response);
            }            
            return Ok(response);
        }
    }
}