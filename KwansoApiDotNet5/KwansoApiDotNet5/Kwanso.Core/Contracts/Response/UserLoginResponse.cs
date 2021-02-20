using Kwanso.Core.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwanso.Core.Contracts.Response
{
   public class UserLoginResponse:BaseResponse
    {
       
        public JwtToken jwt { get; set; }
    }
}
