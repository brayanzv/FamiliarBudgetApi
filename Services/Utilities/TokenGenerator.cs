using Data.DTOs;
using Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class TokenGenerator
    {
        private readonly IConfiguration configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AuthenticationResponse BuildToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim("firstName", user.FirstName.ToString()),
                new Claim("lastName", user.LastName.ToString()),
                new Claim("avatar", user.Avatar.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim("familyId", user.FamilyId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyJwt"]));

            var securityCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: securityCredentials);

            return new AuthenticationResponse()
            {
                UserId= user.UserId,
                FirstName= user.FirstName,
                LastName= user.LastName,
                Avatar = user.Avatar,
                Email= user.Email,
                RoleId = user.RoleId != null ? user.RoleId : 0,
                FamilyId = user.FamilyId != null ? user.FamilyId : 0,
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration,
            };
        }
    }
}
