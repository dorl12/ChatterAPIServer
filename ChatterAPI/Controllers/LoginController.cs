using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ChatterAPI.Controllers
{
    [Route("api/[login]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        public LoginController(IConfiguration config)
        {
            _configuration = config;
        }


        [HttpPost]
        public IActionResult Post([Bind("Id, Password")] User u)
        {
            System.Diagnostics.Debug.WriteLine("Login");
            foreach (User usr in UserDataService._users)
            {
                if (usr.Id == u.Id)
                {
                    if (usr.Password == u.Password)
                    {
                        var claims = new[] {
                                new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWTParams:Subject"]),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                                new Claim("UserId", u.Id)
                            };
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtParams:SecretKey"]));
                        var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["JwtParams:Issuer"],
                            _configuration["JwtParams:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(20),
                            signingCredentials: mac);

                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                    System.Diagnostics.Debug.WriteLine("NotFP");
                    return NotFound("Password Incorrect!");
                }
            }
            System.Diagnostics.Debug.WriteLine("NotFU");
            return NotFound("Username does not exist!");
        }
    }
}
