using Microsoft.AspNetCore.Mvc;

namespace ChatterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost("{id}, {password}, {nickname}, {profileImage}")]
        public IActionResult Post(string id, string password, string nickname, string profileImage)
        {
            foreach (User usr in UserDataService._users)
            {
                if (usr.Id == id)
                {
                    return BadRequest("Username already exist");
                }
            }
            User newUser = new User();
            newUser.Id = id;
            newUser.Password = password;
            newUser.Name = nickname;
            newUser.Image = profileImage;
            UserDataService._users.Add(newUser);
            return Ok("User added");
        }
    }
}
