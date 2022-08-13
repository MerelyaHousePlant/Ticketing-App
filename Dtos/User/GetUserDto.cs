using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketing_App.Dtos.User
{
    public class GetUserDto
    {
        public int UserId {get; set;}
        public string Name {get; set;} = "userus";
        public string Email {get; set;} = "oopsie@yahoo.com";
        public string Address {get; set;} ="ugabuga";
        public List<Tickets>? Tickets {get; set;}
    }
}