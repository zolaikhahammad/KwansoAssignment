using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwanso.Core.Contracts.Request.Tasks
{
    public class DeleteTaskRequest
    {
        public List<int> Ids { get; set; }
    }
}
