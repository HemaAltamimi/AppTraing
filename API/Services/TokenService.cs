using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["SolNowKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims= new List<Claim>{
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.NameId,user.UserName)
            };

            var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha256Signature);

            var tokenData =new SecurityTokenDescriptor{
                Expires =DateTime.Now.AddDays(7),
                Subject= new ClaimsIdentity(claims),
                SigningCredentials =creds
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenData);

            return tokenHandler.WriteToken(token);

        }
    }
}