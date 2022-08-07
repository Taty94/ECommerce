using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Course.ECommerce.WebApi.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Course.ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtConfiguration jwtConfiguration;
        private readonly ILocationInfoApplication locationInfoApp;

        public TokenController(IOptions<JwtConfiguration> options, ILocationInfoApplication locationInfoApp)
        {
            this.jwtConfiguration = options.Value;
            this.locationInfoApp = locationInfoApp;
        }


        [HttpPost]
        public async Task<TokenDto> TokenAsync(UserInput input)
        {
            //1. Validar User.
            var user = await locationInfoApp.GetLocationInfoAsync(input.Email);
            if (user == null)
            {
                throw new NotFoundException("User Info no encontrada");
            }

            var userTest = user.Email;
            if (input.Email!= userTest || input.Password != "F#(k8284")
            {
                throw new AuthenticationException("User or Passowrd incorrect!");
            }

            //2. Generar claims
            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, userTest),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", userTest),
                        //new Claim("Email", user.Email)
                        //Other...
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.Add(jwtConfiguration.Expires),
                signingCredentials: signIn);


            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return new TokenDto { Token=jwt};
        }


    }
}

