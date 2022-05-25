using Microsoft.AspNetCore.Mvc;
using ChatterAPI.Hubs;

namespace ChatterAPI.Controllers
{
    [ApiController]
    [Route("api/invitations")]
    public class InvitationsController : ControllerBase
    {
        private readonly ChatHub chatHub;
        private readonly IUserDataService _userDataService;

        public InvitationsController(ChatHub c, IUserDataService userDataService)
        {
            chatHub = c;
            _userDataService = userDataService;
        }

        [HttpPost]
        public async Task<IActionResult> Invite([Bind("from,to,server")] Invitations inv)
        {
            foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            {
                if (userChats.Username == inv.to)
                {
                    if (userChats.Chats.Where(x => x.ContactUserName.id == inv.from).FirstOrDefault() != null)
                    {
                        return BadRequest("Contact already exist!");
                    }
                    Contact c = new Contact();
                    c.id = inv.from;
                    c.name = inv.from;
                    c.server = inv.server;
                    c.last = "";
                    c.lastdate = new DateTime();
                    await chatHub.SendMessage(inv.from);
                    userChats.Chats.Add(new Chat() { ContactUserName = c, Messages = new List<Message>() });
                    return Ok("invitation - Contact Added");
                }
            }
            return NotFound("Username not in this server");
        }
    };
}