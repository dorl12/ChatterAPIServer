using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ChatterAPI.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public IConfiguration _configuration;
        //private readonly IUserDataService _userDataService;
        private IUserModel userModel = new UserModel();
        public RegisterController(IConfiguration config)
        {
            _configuration = config;
            //_userDataService = userDataService;
        }

        [HttpPost]
        public IActionResult Post([Bind("Id, Password, Name")] User u)
        {
            foreach (User usr in userModel.GetAllUsers())
            {
                if (usr.id == u.id)
                {
                    return BadRequest("Username already exist");
                }
            }
            if(u.id.Length == 0 || u.password.Length == 0 || u.name.Length == 0) {
                return BadRequest("Empty fields are not Allowed");
            }
            bool containsInt = u.password.Any(char.IsDigit);
            bool containsLetter = u.password.Any(char.IsLetter);
            if(!(containsInt && containsLetter))
            {
                return BadRequest("password must contains letters and digits");
            }

            User newUser = new User();
            newUser.id = u.id;
            newUser.password = u.password;
            newUser.name = u.name;
            userModel.AddUser(newUser);
            //UserChats uC = new UserChats();
            //uC.username = newUser.id;
            //uC.chats = new List<Chat>();
            //_userDataService.AddUserChats(uC);
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
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
