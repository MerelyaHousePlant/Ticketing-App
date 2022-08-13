using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ticketing_App.Models;
using Ticketing_App.Services.TicketServices;
using Ticketing_App.Dtos.Ticket;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Ticketing_App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<List<GetTicketDto>>> Get()
        {
            var response = await _ticketService.GetAllTickets();
             if (response.Success == false)
            {
                return BadRequest(response);
            }            
            return Ok(response);
        }
        [HttpGet]
        [Route("GetUserTicket/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<List<GetTicketDto>>> GetTicketsById(int id)
        {
            var response = await _ticketService.GetTicketsById(id);
             if (response.Success == false)
            {
                return BadRequest(response);
            }            
            return Ok(response);
        }
        [HttpPost]
        [Route("AddTicket/{newTicket}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<List<GetTicketDto>>> AddTicket(AddTicketDto newTicket)
        {
           var response = await _ticketService.AddTicket(newTicket);
             if (response.Success == false)
            {
                return BadRequest(response);
            }            
            return Ok(response);
        }
        [HttpPut]
        [Route("EditTicket/{updatedTicket}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<ServiceResponse<GetTicketDto>>> UpdateTicket(UpdateTicketDto updatedTicket)
        {
            var response = await _ticketService.UpdateTicket(updatedTicket);
            if (response.Success == false)
            {
                return BadRequest(response);
            }            
            return Ok(response);
        }
        [HttpDelete("DeleteTicket/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<ServiceResponse<List<GetTicketDto>>>> Delete(int id)
        {
            var response = await _ticketService.DeleteTicket(id);
            if (response.Success == false)
            {
                return BadRequest(response);
            }            
            return Ok(response);
        }
        [HttpPatch]
        [Route("{updatedTicket}/Assign")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<ServiceResponse<GetTicketDto>>> AssignTicket(UpdateTicketDto updatedTicket)
        {
            var response = await _ticketService.AssignTicket(updatedTicket);
             if (response.Success == false)
            {
                return BadRequest(response);
            }            
            return Ok(response);
        }
        
        [HttpGet]
        [Route("{status}/StatusCheck")]
        public async Task<ActionResult<List<GetTicketDto>>> GetTicketsByStatus(Status status)
        {
            var response = await _ticketService.GetTicketsByStatus(status);
             if (response.Success == false)
            {
                return BadRequest(response);
            }            
            return Ok(response);
        }
        [HttpGet]
        [Route("{priority}/PriorityCheck")]
        public async Task<ActionResult<List<GetTicketDto>>> GetTicketsByPriority(Priority priority)
        {
            var response = await _ticketService.GetTicketsByPriority(priority);
             if (response.Success == false)
            {
                return BadRequest(response);
            }            
            return Ok(response);
        }
        [HttpPatch]
        [Route("{updatedTicket}/ChangeTicketStatus")]
        public async Task<ActionResult<GetTicketDto>> ChangeStatus(UpdateTicketDto updatedTicket)
        {
             var response = await _ticketService.ChangeStatus(updatedTicket);
             if (response.Success == false)
            {
                return BadRequest(response);
            }            
            return Ok(response);
        }
        [HttpGet]
        [Route("GetYourTickets")]
        public async Task<ActionResult<List<GetTicketDto>>> GetYourTickets()
        {
         var response = await _ticketService.GetYourTickets();
            if (response.Success == false)
            {
                return BadRequest(response);
            }            
            return Ok(response);
        }
}
}