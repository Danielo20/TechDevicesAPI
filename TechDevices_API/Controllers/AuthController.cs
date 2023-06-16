using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechDevices_API.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;

namespace TechDevices_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string secretKey;
        public AuthController(IConfiguration config)
        {
            secretKey = config.GetSection("Settings").GetSection("SecretKey").ToString();
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult Validate([FromBody] User request)
        {
            if(request.Email == "prueba@gmail.com" && request.Password == "123456")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Email));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string createdToken = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = createdToken });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "invalid credentials" });
            }
        }
    }
}
