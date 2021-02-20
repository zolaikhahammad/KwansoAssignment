using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwansoWebApp.Contracts
{
    public class BaseResponse
    {
        public int status_code { get; set; }
        public string error { get; set; }
    }
}
