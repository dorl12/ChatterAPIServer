using Microsoft.AspNetCore.Mvc;
using ChatterAPI.Hubs;

namespace ChatterAPI.Controllers
{
    [ApiController]
    [Route("api/invitations")]
    public class InvitationsController : ControllerBase
    {
        private readonly ChatHub chatHub;
        private IUserContactsModel userContactsModel = new UserContactsModel();
        private IUserModel userModel = new UserModel();
        private IContactModel contactModel = new ContactModel();
        //private readonly IUserDataService _userDataService;

        public InvitationsController(ChatHub c)
        {
            chatHub = c;
            //_userDataService = userDataService;
        }

        [HttpPost]
        public async Task<IActionResult> Invite([Bind("from,to,server")] Invitations inv)
        {
            if (inv.from == inv.to)
            {
                return BadRequest("Can not chat with yourself!");
            }
            string userId = User.Claims.FirstOrDefault(c => c.Type.EndsWith("UserId"))?.Value;
            User user = userModel.GetUser(inv.to);
            if(user == null)
            {
                return NotFound("Username does not in this server");
            }
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            if (allContactsId.Contains(inv.from))
            {
                return BadRequest("Contact already exist!");
            }
            Contact c = new Contact();
            c.id = inv.from;
            c.name = inv.from;
            c.server = inv.server;
            c.last = "";
            c.lastdate = new DateTime();
            contactModel.AddContact(c);
            userContactsModel.AddContactToUser(c.id, inv.to);
            await chatHub.SendMessage(inv.from);
            return Created("invitation - Contact Added", c);

            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == inv.to)
            //    {
            //        if (userChats.Chats.Where(x => x.ContactUserName.id == inv.from).FirstOrDefault() != null)
            //        {
            //            return BadRequest("Contact already exist!");
            //        }
            //        Contact c = new Contact();
            //        c.id = inv.from;
            //        c.name = inv.from;
            //        c.server = inv.server;
            //        c.last = "";
            //        c.lastdate = new DateTime();
            //        await chatHub.SendMessage(inv.from);
            //        userChats.Chats.Add(new Chat() { ContactUserName = c, Messages = new List<Message>() });
            //        return Created("invitation - Contact Added", c);
            //    }
            //}
            //return NotFound("Username does not in this server");
        }
    };
}