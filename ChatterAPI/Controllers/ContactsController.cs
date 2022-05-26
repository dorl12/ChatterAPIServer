using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ChatterAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly IUserDataService _userDataService;

        public ContactsController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type.EndsWith("UserId"))?.Value;
            List<Contact> allContacts = new List<Contact>();
            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == userId)
                {
                    foreach (Chat chat in userChats.Chats)
                    {
                        allContacts.Add(chat.ContactUserName);
                    }
                }
            }
            return Ok(allContacts);
        }

        [HttpGet("{id}")]
        public IActionResult Detailes(string? id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            Chat toFind = new Chat();
            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == userId)
                {
                    toFind = (userChats.Chats.Where(x => x.ContactUserName.id == id).FirstOrDefault());
                    if (toFind == null)
                    {
                        return NotFound("Contact does not exist!");
                    }
                }
            }
            return Ok(toFind.ContactUserName);
        }

        [HttpPost]
        public IActionResult Create([Bind("name,server,last,lastdate")] Contact contact)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == userId)
                {
                    if (userChats.Chats.Where(x => x.ContactUserName.id == contact.id).FirstOrDefault() != null)
                    {
                        return BadRequest("Contact already exist!");
                    }
                    contact.last = "";
                    contact.lastdate = new DateTime();
                    userChats.Chats.Add(new Chat() { ContactUserName = contact, Messages = new List<Message>() });
                    return Created("Contact Added", contact);
                }
            }
            return Ok("");
        }

        [HttpPut("{id}")]
        public IActionResult Update([Bind("name,server")] Contact contact, string? id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == userId)
                {
                    Contact c = userChats.Chats.Where(x => x.ContactUserName.id == id).FirstOrDefault().ContactUserName;
                    if (c == null)
                    {
                        return NotFound("Contact does not exist!");
                    }
                    c.name = contact.name;
                    c.server = contact.server;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string? id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            Chat toFind = new Chat();
            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == userId)
                {
                    toFind = (userChats.Chats.Where(x => x.ContactUserName.id == id).FirstOrDefault());
                    if (toFind == null)
                    {
                        return NotFound("Contact does not exist!");
                    }
                    userChats.Chats.Remove(toFind);
                    break;
                }
            }
            return NoContent();
        }
    }}