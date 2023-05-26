using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DBLApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace DBLApi.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetUserIdFromToken(string token)
        {
            var principal = GetPrincipalFromToken(token);
            var userId = principal?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            return userId ?? "";
        }

        public string GetUsernameFromToken(string token)
        {
            var principal = GetPrincipalFromToken(token);
            var username = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return username ?? "";
        }

        public string GetUserRoleFromToken(string token)
        {
            var principal = GetPrincipalFromToken(token);
            var role = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            return role ?? "";
        }

        private ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);
            try
            {
                var principal = tokenHandler.ValidateToken(token, new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }

    }
}