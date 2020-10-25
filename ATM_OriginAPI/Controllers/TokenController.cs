using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ATM_Origin.Core.Entities;
//using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ATM_Origin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authentication(UserLogin login)
        {
            if(IsValidUser(login))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }
            return NotFound();

        }

        private bool IsValidUser(UserLogin login)
        {
            return true;
        }
        private string GenerateToken()
        {
            //return string.Empty;
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims (informacion a validar, agregar en el cuerpo)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Marcelo Ajalla"),
                new Claim(ClaimTypes.Email, "marcajal86@gmail.com"),
                new Claim(ClaimTypes.Role, "Administrador"),
            };

            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(2)   
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
