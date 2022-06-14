using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ChatterAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        //private readonly IUserDataService _userDataService;
        private IUserModel userModel = new UserModel();

        //public UsersController(IUserDataService userDataService)
        //{
        //    //_userDataService = userDataService;
        //}

        [HttpGet]
        public IActionResult Index()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type.EndsWith("UserId"))?.Value;
            
            foreach (User user in userModel.GetAllUsers())
            {
                if (user.id == userId)
                {
                    return Ok(user);
                }
            }
            return NotFound("User not found");
        }

        [HttpGet("{id}")]
        public IActionResult Detailes(string? id)
        {
            foreach (User user in userModel.GetAllUsers())
            {
                if (user.id == id)
                {
                    return Ok(user);
                }
            }
            return NotFound("User not found");
        }
    }}