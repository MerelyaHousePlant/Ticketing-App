using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketing_App.Dtos.Ticket;
using AutoMapper;
using Ticketing_App.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ticketing_App.Services.TicketServices
{
    public class TicketService : ITicketService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TicketService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
        .FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetTicketDto>>> AddTicket(AddTicketDto newTicket)
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();
            try
            {
                if (checkIfAdmin(GetUserId()) == true)
                {
                    Tickets ticket = _mapper.Map<Tickets>(newTicket);
                    _context.Tickets.Add(ticket);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Tickets
                    .Select(c => _mapper.Map<GetTicketDto>(c))
                    .ToListAsync();
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Not Admin";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> DeleteTicket(int id)
        {
            ServiceResponse<List<GetTicketDto>> response = new ServiceResponse<List<GetTicketDto>>();
            try
            {
                if (checkIfAdmin(GetUserId()) == true)
                {
                    Tickets ticket = _context.Tickets.First(c => c.TicketId == id);
                    if (ticket != null)
                    {
                        _context.Tickets.Remove(ticket);
                        await _context.SaveChangesAsync();
                    }
                    //response.Data = _context.Tickets.Select(c => _mapper.Map<GetTicketDto>(c)).ToList();
                    //if (response.Data != null) response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Admin";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> GetAllTickets()
        {
            var response = new ServiceResponse<List<GetTicketDto>>();
            try
            {
                //if (checkIfAdmin(GetUserId()) == true)
               // {
                    var dbTickets = await _context.Tickets.ToListAsync();
                    response.Data = dbTickets.Select(c => _mapper.Map<GetTicketDto>(c)).ToList();
                    if (response.Data != null) response.Success = true;
               // }
             //   else
             //   {
              //      response.Success = false;
              //      response.Message = "Not Admin";
              //  }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public bool checkIfAdmin(int userId)
        {
            var dbUser = _context.Users
            .Where(c => c.UserId == userId && c.Admin == true);
            if (dbUser != null)
            {
                return true;
            }
            return false;
        }
        public async Task<ServiceResponse<List<GetTicketDto>>> GetYourTickets()
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();
            try
            {
                var dbTicket = await _context.Tickets
                .Where(c => c.Worker == GetUserId())
                .ToListAsync();
                serviceResponse.Data = dbTicket.Select(c => _mapper.Map<GetTicketDto>(c)).ToList();
                if (serviceResponse.Data != null) serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetTicketDto>>> GetTicketsById(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();
            try
            {
                if (checkIfAdmin(GetUserId()) == true)
                {
                    var dbTicket = await _context.Tickets
                    .Where(c => c.Worker == id)
                    .ToListAsync();
                    serviceResponse.Data = dbTicket.Select(c => _mapper.Map<GetTicketDto>(c)).ToList();
                    if (serviceResponse.Data != null) serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Not Admin";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDto>> UpdateTicket(UpdateTicketDto updatedTicket)
        {
            ServiceResponse<GetTicketDto> response = new ServiceResponse<GetTicketDto>();
            try
            {
                if (checkIfAdmin(GetUserId()) == true)
                {
                    var ticket = await _context.Tickets
                    .FirstOrDefaultAsync(c => c.TicketId == updatedTicket.TicketId);
                    ticket.Name = updatedTicket.Name;
                    ticket.Description = updatedTicket.Description;
                    ticket.Owner = updatedTicket.Owner;
                    ticket.Worker = updatedTicket.Worker;
                    ticket.Status = updatedTicket.Status;
                    ticket.Priority = updatedTicket.Priority;
                    ticket.DueData = updatedTicket.DueData;
                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetTicketDto>(ticket);
                    response.Success = true;
                }
                else response.Message = "Not Admin";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }
        public async Task<ServiceResponse<GetTicketDto>> AssignTicket(UpdateTicketDto updatedTicket)
        {
            ServiceResponse<GetTicketDto> response = new ServiceResponse<GetTicketDto>();
            try
            {
                if (checkIfAdmin(GetUserId()) == true)
                {
                    var ticket = await _context.Tickets
                    .FirstOrDefaultAsync(c => c.TicketId == updatedTicket.TicketId);
                    ticket.Worker = updatedTicket.Worker;
                    ticket.Owner = GetUserId();
                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetTicketDto>(ticket);
                    if (response.Data != null) response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Admin";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<GetTicketDto>> ChangeStatus(UpdateTicketDto updatedTicket)
        {
            ServiceResponse<GetTicketDto> response = new ServiceResponse<GetTicketDto>();
            try
            {
                var ticket = await _context.Tickets
                .FirstOrDefaultAsync(c => c.TicketId == updatedTicket.TicketId && c.Worker == GetUserId());
                ticket.Status = updatedTicket.Status;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetTicketDto>(ticket);
                if (response.Data != null) response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<List<GetTicketDto>>> GetTicketsByStatus(Status status)
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();
            try
            {
                var dbTicket = await _context.Tickets
                .Where(c => c.Status == status)
                .ToListAsync();
                serviceResponse.Data = dbTicket.Select(c => _mapper.Map<GetTicketDto>(c)).ToList();
                if (serviceResponse.Data != null) serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetTicketDto>>> GetTicketsByPriority(Priority priority)
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();
            try
            {
                var dbTicket = await _context.Tickets
                .Where(c => c.Priority == priority)
                .ToListAsync();
                serviceResponse.Data = dbTicket.Select(c => _mapper.Map<GetTicketDto>(c)).ToList();
                if (serviceResponse.Data != null) serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
