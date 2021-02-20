using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwansoWebApp.Contracts.User.Request
{
    public class UserLoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
        
    }
}
