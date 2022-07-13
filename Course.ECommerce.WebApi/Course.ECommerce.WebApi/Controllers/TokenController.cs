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
        private static readonly User[] usuarios = new[]
        {
            new User{UserName="Tatiana", Role=new Role[] {Role.Admin}, Licencia=true, Ecuatoriano=true, CodigoSeguro="EC12345" },
            new User{UserName="Juan", Role=new Role[] {Role.User, Role.Soporte} , Licencia=false, Ecuatoriano=true, CodigoSeguro="EC09876" },
            new User{UserName="Pedro", Role=new Role[] {Role.Invitado},Licencia=false, Ecuatoriano=false, CodigoSeguro="EC65789"  },
        };

        public TokenController(IOptions<JwtConfiguration> options)
        {
            this.jwtConfiguration = options.Value;
        }


        [HttpPost]
        public async Task<string> TokenAsync(UserInput input)
        {

            //1. Validar User.
            //var userTest = "foo";
            var user = usuarios.Where(u => u.UserName.Equals(input.UserName)).SingleOrDefault();
            if (!user.UserName.Equals(input.UserName) || input.Password != "123")
            {
                throw new AuthenticationException("User or Passowrd incorrect!");
            }


            //2. Generar claims
            //create claims details based on the user information
            var claims = new List<Claim>();

            var usr = usuarios.Where(u => u.UserName.Equals(input.UserName)).SingleOrDefault();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usr.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()));

            foreach(var role in usr.Role)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            claims.Add(new Claim("Ecuatoriano", usr.Ecuatoriano.ToString()));
            claims.Add(new Claim("Licencia", usr.Licencia.ToString()));
            claims.Add(new Claim("Seguro", usr.CodigoSeguro));


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.Add(jwtConfiguration.Expires),
                signingCredentials: signIn);


            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);


            return jwt;
        }


    }
}

