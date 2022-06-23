using Microsoft.AspNetCore.Mvc;
using ChatterAPI.Hubs;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.IO;
using System.Threading.Tasks;

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
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("chatter-4d962-firebase-adminsdk-wc8ds-95669a0a7f.json")
                });
            }
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

            if(FirebaseController.firebaseTokens.ContainsKey(inv.to))
            {
                var token = FirebaseController.firebaseTokens[inv.to];


                if (token != null)
                {
                    var message = new Message()
                    {
                        Data = new Dictionary<string, string>()
                {
                    { "type", "contact" },
                },
                        Token = token,
                        Notification = new Notification()
                        {
                            Title = "From: " + inv.from,
                            Body = inv.server
                        }

                    };
                    FirebaseMessaging.DefaultInstance.SendAsync(message);
                }
            }
            

            return Created("invitation - Contact Added", c);
        }
    };
}