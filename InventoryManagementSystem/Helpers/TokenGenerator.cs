
using InventoryManagementSystem.Constants;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagementSystem.Helpers
{
    public static class TokenGenerator
    {
        public static string GenerateToken(UserDto userDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", userDTO.Id.ToString()),
                    new Claim("Role", Role.Customer.ToString()),
                    new Claim("UserName", userDTO.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(JwtSettings.DurationInMinutes),
                Issuer = JwtSettings.Issuer,
                Audience = JwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
