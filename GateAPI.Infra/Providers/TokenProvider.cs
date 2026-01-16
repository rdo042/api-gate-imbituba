using GateAPI.Application.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GateAPI.Infra.Providers
{
    public class TokenProvider(IConfiguration config) : ITokenProvider
    {
        private readonly IConfiguration _config = config;

        public string GenerateToken()
        {
            var claims = new List<Claim>
        {
            //new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            //new(JwtRegisteredClaimNames.Email, user.Email),
            //new(ClaimTypes.Role, user.Role) // Se tiver roles
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
