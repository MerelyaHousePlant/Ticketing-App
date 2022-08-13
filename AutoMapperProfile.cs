using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ticketing_App.Dtos.Ticket;
using Ticketing_App.Dtos.User;

namespace Ticketing_App
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tickets, GetTicketDto>();
            CreateMap<AddTicketDto, Tickets>();
            CreateMap<UpdateTicketDto, Tickets>();
            CreateMap<User, GetUserDto>();
            CreateMap<UpdateUserDto, User>();
        }
        
    }
}