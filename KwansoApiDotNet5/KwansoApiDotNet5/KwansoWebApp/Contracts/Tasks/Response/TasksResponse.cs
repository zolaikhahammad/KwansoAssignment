using KwansoWebApp.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwansoWebApp.Contracts.Tasks.Response
{
    public class TasksResponse:BaseResponse
    {
        public TaskViewModel task { get; set; }
        public List<TaskViewModel> tasks { get; set; }
    }
}
