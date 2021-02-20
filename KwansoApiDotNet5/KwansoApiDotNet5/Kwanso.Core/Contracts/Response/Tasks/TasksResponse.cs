using Kwanso.Core.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwanso.Core.Contracts.Response.Tasks
{
    public class TasksResponse:BaseResponse
    {
        public TaskViewModel task { get; set; }
        public List<TaskViewModel> tasks { get; set; }
    }
}
