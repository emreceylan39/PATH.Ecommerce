using LoginService.Domain.DTOs.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginService.Application.Abstractions
{
    public interface IAuthService
    {
        Task<UserDto> Login(string email, string password);


    }
}
