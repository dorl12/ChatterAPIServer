using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ChatterAPI.Controllers
{
    [Route("api/[register]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public IConfiguration _configuration;
        public RegisterController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost("{id}&{password}&{repeatPassword}&{nickname}")]
        public IActionResult Post(string id, string password, string repeatPassword, string nickname)
        {
            System.Diagnostics.Debug.WriteLine("Register");
            foreach (User usr in UserDataService._users)
            {
                if (usr.Id == id)
                {
                    return BadRequest("Username already exist");
                }
            }
            if(id.Length == 0 || password.Length == 0 || repeatPassword.Length == 0 || nickname.Length == 0) {
                return BadRequest("Empty fill is not Allowed");
            }
            if (password != repeatPassword)
            {
                return BadRequest("password and repeatPassword most be the same");
            }
            bool containsInt = password.Any(char.IsDigit);
            bool containsLetter = password.Any(char.IsLetter);
            if(!(containsInt && containsLetter))
            {
                return BadRequest("password must contains letters and digits");
            }

            User newUser = new User();
            newUser.Id = id;
            newUser.Password = password;
            newUser.Name = nickname;
            //newUser.Image = profileImage;
            UserDataService._users.Add(newUser);
            UserChats uC = new UserChats();
            uC.Username = newUser.Id;
            uC.Chats = new List<Chat>();
            UserDataService._AllUsersChats.Add(uC);
            var claims = new[] {
                                new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWTParams:Subject"]),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                                new Claim("UserId", id)
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
