using Microsoft.AspNetCore.Mvc;
using ChatterAPI.Hubs;

namespace ChatterAPI.Controllers
{
    [ApiController]
    [Route("api/transfer")]
    public class TransferController : ControllerBase
    {
        private readonly ChatHub chatHub;

        public TransferController(ChatHub c) { chatHub = c; }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("to,from,content")] Transfer transfer)
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
                            await chatHub.SendMessage(transfer.content);
                            return Ok("Success!");
                        }
                    }
                }
            }
            return NotFound("Contact does not exist!");
        }

    };
}