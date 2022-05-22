using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ChatterAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        //public static List<Contact> _contacts = new List<Contact>()
        //{new Contact() { id = "or", name = "Or Drukman", server = "localhost:7265", last = "text", lastdate = new DateTime(2008, 5, 1, 8, 30, 52) } };


        [HttpGet]
        public IActionResult Index()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type.EndsWith("UserId"))?.Value;
            
            foreach (User user in UserDataService._users)
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
            foreach (User user in UserDataService._users)
            {
                if (user.Id == id)
                {
                    return Ok(user);
                }
            }
            return NotFound("User not found");
        }

        //[HttpPost]
        //public IActionResult Create([Bind("name,server,last,lastdate")] Contact contact)
        //{
        //    string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        //    //string userId = "or";
        //    foreach (UserChats userChats in UserDataService._AllUsersChats)
        //    {
        //        if (userChats.Username == userId)
        //        {
        //            if (userChats.Chats.Where(x => x.ContactUserName.id == contact.id).FirstOrDefault() != null)
        //            {
        //                return BadRequest("Contact already exist!");
        //            }
        //            contact.last = "";
        //            contact.lastdate = new DateTime();
        //            userChats.Chats.Add(new Chat() { ContactUserName = contact, Messages = new List<Message>() });
        //            return Ok("Contact Added");
        //        }
        //    }
        //    return Ok("nothing to say");
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update([Bind("name,server")] Contact contact, string? id)
        //{
        //    string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        //    //string userId = "or";
        //    foreach (UserChats userChats in UserDataService._AllUsersChats)
        //    {
        //        if (userChats.Username == userId)
        //        {
        //            Contact c = userChats.Chats.Where(x => x.ContactUserName.id == id).FirstOrDefault().ContactUserName;
        //            if (c == null)
        //            {
        //                return NotFound("Contact not found exist!");
        //            }
        //            c.name = contact.name;
        //            c.server = contact.server;
        //        }
        //    }
        //    return Ok("Contact updated");
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(string? id)
        //{
        //    string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        //    //string userId = "or";
        //    Chat toFind = new Chat();
        //    foreach (UserChats userChats in UserDataService._AllUsersChats)
        //    {
        //        if (userChats.Username == userId)
        //        {
        //            toFind = (userChats.Chats.Where(x => x.ContactUserName.id == id).FirstOrDefault());
        //            if (toFind == null)
        //            {
        //                return NotFound("Contact does not exist!");
        //            }
        //            userChats.Chats.Remove(toFind);
        //            break;
        //        }
        //    }
        //    return Ok("Contact deleted");
        //}
    };
}