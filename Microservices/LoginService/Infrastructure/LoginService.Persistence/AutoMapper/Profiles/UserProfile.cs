using AutoMapper;
using LoginService.Domain.DTOs.Login;
using LoginService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginService.Persistence.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
