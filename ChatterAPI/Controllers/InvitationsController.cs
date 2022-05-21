using Microsoft.AspNetCore.Mvc;
using ChatterAPI.Controllers;

namespace ChatterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Invite([Bind("from,to,server")] Invitations inv)
        {
            foreach (UserChats userChats in UserDataService._AllUsersChats)
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
                    userChats.Chats.Add(new Chat() { ContactUserName = c, Messages = new List<Message>() });
                    return Ok("invitation - Contact Added");
                }
            }
            return NotFound("Username not in this server");
        }
    };
}