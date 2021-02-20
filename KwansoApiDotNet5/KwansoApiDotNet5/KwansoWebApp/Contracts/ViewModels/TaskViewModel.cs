using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwansoWebApp.Contracts.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
