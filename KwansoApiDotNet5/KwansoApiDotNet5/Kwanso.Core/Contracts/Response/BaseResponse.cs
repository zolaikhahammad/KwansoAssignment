using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwanso.Core.Contracts.Response
{
    public class BaseResponse
    {
        public int status_code { get; set; }
        public string error { get; set; }
        
    }
}
