using LoginService.Domain.DTOs.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LoginService.API.Security
{
    public class TokenHandler
    {
        //public static IConfiguration _configuration;
        public static IConfiguration _configuration;
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Token CreateAccessToken(UserDto loginResponse)
        {

            Token tokenInstance = new Token();
            //Security  Key'in simetriğini alıyoruz.

        string securityKey = "aG9yc2V0aWxsbmF0aW9udGlnaHRseXNwZWNpYWxwaWNrZmlyZXBsYWNlc2hvcnRudXQ=";
            SymmetricSecurityKey signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId", loginResponse.Id.ToString())
               

            };
            //claims.Add(new Claim(ClaimTypes.NameIdentifier,logInResponse.Id.ToString()));

            //Oluşturulacak token ayarlarını veriyoruz.
            tokenInstance.Expiration = DateTime.Now.AddMinutes(5);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                issuer: "https://localhost:2000",
                //audience: "PATHCOMTR",
                expires: tokenInstance.Expiration,//Token süresini 5 dk olarak belirliyorum
                notBefore: DateTime.Now,//Token üretildikten ne kadar süre sonra devreye girsin ayarlıyouz.
                signingCredentials: signingCredentials

                );
            //Token oluşturucu sınıfında bir örnek alıyoruz.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Token üretiyoruz.
            tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenInstance.RefreshToken = CreateRefreshToken();

            return tokenInstance;



        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
