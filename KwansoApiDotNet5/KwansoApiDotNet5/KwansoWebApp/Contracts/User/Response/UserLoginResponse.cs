using KwansoWebApp.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwansoWebApp.Contracts.User.Response
{
    public class UserLoginResponse:BaseResponse
    {
        public JwtToken jwt { get; set; }
    }
}
