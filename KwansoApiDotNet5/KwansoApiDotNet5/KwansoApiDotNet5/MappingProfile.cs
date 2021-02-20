using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwanso.Core.Models;
using Kwanso.Core.Contracts.ViewModels;

namespace KwansoApiDotNet5
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Users, UsersViewModel>().ReverseMap();
            CreateMap<Tasks, TaskViewModel>().ReverseMap();

        }
    }
}
