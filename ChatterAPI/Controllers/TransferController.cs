using Microsoft.AspNetCore.Mvc;

namespace ChatterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([Bind("to,from,content")] Transfer transfer)
        {
            Message message = new Message();
            message.sent = false;
            message.created = DateTime.Now;
            message.content = transfer.content;
            foreach (UserChats userChats in UserDataService._AllUsersChats)
            {
                if (userChats.Username == transfer.to)
                {
                    foreach (Chat chat in userChats.Chats)
                    {
                        if (chat.ContactUserName.id == transfer.from)
                        {
                            message.id = -1;
                            chat.Messages.Add(message);
                            chat.ContactUserName.last = message.content;
                            chat.ContactUserName.lastdate = message.created;
                            return Ok("Success!");
                        }
                    }
                }
            }
            return NotFound("Contact does not exist!");
        }

    };
}