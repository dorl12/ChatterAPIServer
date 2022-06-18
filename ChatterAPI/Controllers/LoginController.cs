using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ChatterAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        //private readonly IUserDataService _userDataService;
        private IUserModel userModel = new UserModel();
        public LoginController(IConfiguration config)
        {
            _configuration = config;
            //_userDataService = userDataService;
        }

        [HttpPost]
        public IActionResult Post([Bind("Id, Password")] User u)
        {
            foreach (User usr in userModel.GetAllUsers())
            {
                if (usr.id == u.id)
                {
                    if (usr.password == u.password)
                    {
                        var claims = new[] {
                                new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWTParams:Subject"]),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                                new Claim("UserId", u.id)
                            };
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtParams:SecretKey"]));
                        var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["JwtParams:Issuer"],
                            _configuration["JwtParams:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(20),
                            signingCredentials: mac);

                        return NotFound(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                    return BadRequest("Password Incorrect!");
                }
            }
            return BadRequest("Username does not exist!");
        }
    }
}
