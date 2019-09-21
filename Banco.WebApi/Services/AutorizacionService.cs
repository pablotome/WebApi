using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Banco.WebApi.DTOs.Requests;
using Banco.WebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Banco.WebApi.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly IConfiguration _configuration;
        private readonly IEnumerable<Usuario> _users = new List<Usuario>
        {
            new Usuario { Mail = "admin1@mail.com", Password = "!as123456s", Rol = Rol.Administrador},
            new Usuario { Mail = "admin2@mail.com", Password = "!as123456s", Rol = Rol.Administrador},
            new Usuario { Mail = "user@mail.com", Password = "!as123456s", Rol = Rol.Usuario},
        };

        public AutorizacionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            var user = _users.SingleOrDefault(x =>
                x.Mail == loginDTO.Mail &&
                x.Password == loginDTO.Password);

            if (user == null)
                return null;

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:ClaveSecreta"]);
            var min = int.Parse(_configuration["Jwt:MinutosDeExpiracion"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(min),
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("userid", user.Mail),
                    new Claim("role", user.Rol.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return JsonConvert.SerializeObject(token);
        }
    }
}
