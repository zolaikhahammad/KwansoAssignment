using Kwanso.Core.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwanso.Core.Contracts.Response.Users
{
    public class UsersReponse:BaseResponse
    {
        public UsersViewModel user { get; set; }
    }
}
