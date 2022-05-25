using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ChatterAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserDataService _userDataService;

        public UsersController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type.EndsWith("UserId"))?.Value;
            
            foreach (User user in _userDataService.GetAllUsers())
            {
                if (user.Id == userId)
                {
                    return Ok(user);
                }
            }
            return NotFound("User not found");
        }

        [HttpGet("{id}")]
        public IActionResult Detailes(string? id)
        {
            foreach (User user in _userDataService.GetAllUsers())
            {
                if (user.Id == id)
                {
                    return Ok(user);
                }
            }
            return NotFound("User not found");
        }
    }}