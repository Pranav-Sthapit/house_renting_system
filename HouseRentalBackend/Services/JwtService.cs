using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HouseRentalBackend.Services
{
    public class JwtService
    {
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration configuration) => this.configuration = configuration;

        public string GenerateToken(int userid,string username,string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userid.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                        Convert.ToDouble(configuration["JwtSettings:DurationInMinutes"])    
                    ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
