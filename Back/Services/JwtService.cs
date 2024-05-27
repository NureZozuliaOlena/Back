using Back.Interfaces;
using Back.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace Back.Services
{
    public class JwtService : IJwtService
    {
    public string CreateToken(UserBase user)
            {
                var token = CreateJwtToken(
                    CreateClaims(user),
                    CreateSigningCredentials()
                );

                var tokenHandler = new JwtSecurityTokenHandler();

                return tokenHandler.WriteToken(token);
            }

            private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials) =>
                new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials
                );

            private Claim[] CreateClaims(UserBase user) =>
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("Id", user.Id.ToString())
                };

            private SigningCredentials CreateSigningCredentials() =>
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("secretKeyPassword")
                    ),
                    SecurityAlgorithms.HmacSha256
                );
    }
}
