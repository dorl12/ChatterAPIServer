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
    [Route("api/transfer")]
    public class TransferController : ControllerBase
    {
        private readonly ChatHub chatHub;
        private IUserContactsModel userContactsModel = new UserContactsModel();
        private ImessageDBModel messageDBModel = new MessageDBModel();
        private IChatMessagesModel chatMessagesModel = new ChatMessagesModel();
        private IUserModel userModel = new UserModel();
        //private readonly IUserDataService _userDataService;

        public TransferController(ChatHub c)
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
        public async Task<IActionResult> Create([Bind("to,from,content")] Transfer transfer)
        {
        
             List<string> allContactsId =  userContactsModel.GetAllUserContacts(transfer.to);
            if (!allContactsId.Contains(transfer.from))
            {
                return NotFound("Contact does not exist!");
            }
            List<String> allUsersId =  userModel.GetAllUserIds();
            int newMessageId = -1;
            if (allUsersId.Contains(transfer.from))
            {
                Thread.Sleep(500);
                List<UserContacts> userContacts =  userContactsModel.GetAllUsersContacts();
                foreach (var chat in userContacts)
                {
                    if (chat.userId == transfer.from && chat.contactId == transfer.to)
                    {
                        if (chat.lastMessageId == 0) { chat.lastMessageId = 1; }
                        //user.lastMessageId = newMessageId;
                        newMessageId = chat.lastMessageId;
                    }
                }
            } else
            {
                MessageDB newMessage = new MessageDB();
                //newMessage.id = message.id;
                newMessage.content = transfer.content;
                newMessage.created = DateTime.Now;
                newMessage.from = transfer.from;
                newMessage.to = transfer.to;
                newMessageId = messageDBModel.AddMessage(newMessage, transfer.from, transfer.to);
            }

            int userContactId = 0;
            List<UserContacts> chats = userContactsModel.GetAllUsersContacts();
            foreach (var chat in chats)
            {
                if (chat.userId == transfer.to && chat.contactId == transfer.from)
                {
                    //user.lastMessageId = newMessageId;
                    userContactsModel.UpdateContact(chat.id, newMessageId);
                    userContactId = chat.id;
                }
            }
            IContactModel contactModel = new ContactModel();
            //contactModel.UpdateLastMessage(transfer.from, transfer.content, newMessage.created); // REMOVE?
            chatMessagesModel.AddMessage(userContactId, newMessageId);

            if (FirebaseController.firebaseTokens.ContainsKey(transfer.to))
            {
                var token = FirebaseController.firebaseTokens[transfer.to];
                if (token != null)
                {
                    var message = new Message()
                    {
                        Data = new Dictionary<string, string>()
                {
                    { "type", "message" },
                },
                        Token = token,
                        Notification = new Notification()
                        {
                            Title = "From: " + transfer.from,
                            Body = transfer.content
                        }

                    };
                    await FirebaseMessaging.DefaultInstance.SendAsync(message);
                }
            }


            await chatHub.SendMessage(transfer.content);
            return Created("Message sent succesfully!", transfer.content);


        }
    };
}