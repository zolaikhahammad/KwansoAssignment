using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwanso.Core.Contracts.Request.Users
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Email
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }
    }
}
