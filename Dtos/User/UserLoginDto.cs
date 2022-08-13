using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketing_App.Dtos.User
{
    public class UserLoginDto
    {
        public string Name {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;
    }
}