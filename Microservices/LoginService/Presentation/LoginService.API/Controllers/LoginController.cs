using Azure;
using LoginService.API.Security;
using LoginService.Application.Abstractions;
using LoginService.Domain.DTOs.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public LoginController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;

        }

        private static List<UserDto> userList = new List<UserDto>();

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            UserDto? user = await _authService.Login(email: loginRequest.Email, password: loginRequest.Password);
            if (user == null)
            {
                return Ok("User is not found");
            }
            TokenHandler tokenHandler = new TokenHandler(_configuration);
            Token token = tokenHandler.CreateAccessToken(user);

            user.AccessToken = token.AccessToken;
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenEndDate = token.Expiration.AddMinutes(3);

            userList.Remove(userList.FirstOrDefault(u => u.Id == user.Id));
            userList.Add(user);


            return Ok(user);
        }

        [Route("RefreshToken")]
        [HttpPost]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            if (refreshTokenRequest.RefreshToken == null || refreshTokenRequest.RefreshToken == string.Empty)
                return BadRequest("Refresh token is empty.");

            UserDto user = userList.FirstOrDefault(u => u.RefreshToken == refreshTokenRequest.RefreshToken);

            if (user != null && user.RefreshTokenEndDate > DateTime.Now)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(3);
                user.AccessToken = token.AccessToken;

                userList.Remove(user);
                userList.Add(user);

                return Ok(token);

            }
            else
            {
                return BadRequest("Token is expired");
            }

        }

        [Authorize]
        [Route("ActiveUsers")]
        [HttpGet]
        public IActionResult GetActiveUsers()
        {
            if (userList.Count >= 1 && userList.Any(user => user.RefreshTokenEndDate >= DateTime.Now))
                return Ok(userList.Where(user => user.RefreshTokenEndDate >= DateTime.Now));
            return NoContent();
        }
    }
}

