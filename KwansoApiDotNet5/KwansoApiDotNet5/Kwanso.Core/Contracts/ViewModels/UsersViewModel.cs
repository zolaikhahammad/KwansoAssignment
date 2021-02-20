using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwanso.Core.Contracts.ViewModels
{
    public class UsersViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }    

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
