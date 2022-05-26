using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace ChatterAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/contacts/{contact}/messages")]
    public class MessagesController : ControllerBase
    {
        private static int MessageID = 0;
        private readonly IUserDataService _userDataService;

        public MessagesController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [HttpGet]
        public IActionResult Index(string contact)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == userId)
                {
                    foreach (Chat chat in userChats.Chats)
                    {
                        if (chat.ContactUserName.id == contact)
                        {
                            return Ok(chat.Messages);
                        }
                    }
                }
            }
            return NotFound("Contact does not exist!");
        }

        [HttpGet("{m_id}")]
        public IActionResult Detailes(string contact, int m_id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == userId)
                {
                    foreach (Chat chat in userChats.Chats)
                    {
                        if (chat.ContactUserName.id == contact)
                        {
                            if (chat.Messages.Where(x => x.id == m_id).FirstOrDefault() == null)
                            {
                                return NotFound("Message does not exist!");
                            }
                            return Ok(chat.Messages.Where(x => x.id == m_id).FirstOrDefault());
                        }
                    }
                }
            }
            return NotFound("Contact does not exist!");
        }

        [HttpPost]
        public IActionResult Create([Bind("content")] Message message, string contact)
        {
            message.sent = true;
            message.created = DateTime.Now;
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == userId)
                {
                    foreach (Chat chat in userChats.Chats)
                    {
                        if (chat.ContactUserName.id == contact)
                        {
                            message.id = MessageID++;
                            chat.Messages.Add(message);
                            chat.ContactUserName.last = message.content;
                            chat.ContactUserName.lastdate = message.created;
                            return Created("Message Added", message);
                        }
                    }
                }
            }
            return NotFound("Contact does not exist!");
        }

        [HttpPut("{m_id}")]
        public IActionResult Update([Bind("content")] Message message, string contact, int? m_id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == userId)
                {
                    foreach (Chat chat in userChats.Chats)
                    {
                        if (chat.ContactUserName.id == contact)
                        {
                            foreach (Message mes in chat.Messages)
                            {
                                if (mes.id == m_id)
                                {
                                    mes.content = message.content;
                                    NoContent();
                                }
                            }
                            return NotFound("Message does not exist!");
                        }
                    }
                }
            }
            return NotFound("Contact does not exist!");
        }

        [HttpDelete("{m_id}")]
        public IActionResult Delete(string contact, int? m_id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == userId)
                {
                    foreach (Chat chat in userChats.Chats)
                    {
                        if (chat.ContactUserName.id == contact)
                        {
                            foreach (Message mes in chat.Messages)
                            {
                                if (mes.id == m_id)
                                {
                                    chat.Messages.Remove(mes);
                                    return NoContent();
                                }
                            }
                            return NotFound("Message does not exist!");
                        }
                    }
                }
            }
            return NotFound("Contact does not exist!");
        }
    };
}