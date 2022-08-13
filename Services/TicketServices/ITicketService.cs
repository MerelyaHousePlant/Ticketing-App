using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketing_App.Dtos.Ticket;

namespace Ticketing_App.Services.TicketServices
{
    public interface ITicketService
    {
        Task<ServiceResponse<List<GetTicketDto>>> GetAllTickets();
        Task<ServiceResponse<List<GetTicketDto>>> GetTicketsById(int id);
        Task<ServiceResponse<List<GetTicketDto>>> AddTicket(AddTicketDto newTicket);
        Task<ServiceResponse<GetTicketDto>> UpdateTicket(UpdateTicketDto updatedTicket);
        Task<ServiceResponse<List<GetTicketDto>>> DeleteTicket(int id);
        Task<ServiceResponse<GetTicketDto>> AssignTicket(UpdateTicketDto updatedTicket);
        Task<ServiceResponse<List<GetTicketDto>>> GetTicketsByStatus(Status status);
        Task<ServiceResponse<List<GetTicketDto>>> GetTicketsByPriority(Priority priority);
        Task<ServiceResponse<GetTicketDto>> ChangeStatus(UpdateTicketDto updatedTicket);
        Task<ServiceResponse<List<GetTicketDto>>> GetYourTickets();
    }
}