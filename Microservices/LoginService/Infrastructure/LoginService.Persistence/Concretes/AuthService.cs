using AutoMapper;
using LoginService.Application.Abstractions;
using LoginService.Domain.Contexts;
using LoginService.Domain.DTOs.Login;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginService.Persistence.Concretes
{
    public class AuthService : IAuthService
    {
        private readonly LoginDbContext _context;
        private readonly IMapper _mapper;
        public AuthService(LoginDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password==password);
            if (user == null)
            {
                return null;
            }

            var map = _mapper.Map<UserDto>(user);

            return map;


        }
    }
}
