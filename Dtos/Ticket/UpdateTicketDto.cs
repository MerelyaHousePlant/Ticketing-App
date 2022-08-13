using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketing_App.Dtos.Ticket
{
    public class UpdateTicketDto
    {
        public int TicketId {get; set;}
        public string Name {get; set;} = "templateus";
        public string Description {get; set;} = "Descriptus";
        public int Owner {get; set;}
        public int Worker {get; set;}
        public Status Status {get; set;} = Status.To_Do;
        public Priority Priority {get; set;} = Priority.Low;
        public DateTime DueData {get; set;}
        
    }
}