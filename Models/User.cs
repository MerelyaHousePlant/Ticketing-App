using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketing_App.Models
{
    public class User
    {
        public int UserId {get; set;}
        public string Name {get; set;} = "userus";
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
        public string Email {get; set;} = "oopsie@yahoo.com";
        public string Address {get; set;} ="ugabuga";
        public bool Admin {get; set;}
        public List<Tickets>? Tickets {get; set;}
    }
}