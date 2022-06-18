using Microsoft.AspNetCore.Mvc;
using ChatterAPI.Hubs;

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
        //private readonly IUserDataService _userDataService;

        public TransferController(ChatHub c)
        {
            chatHub = c;
            //_userDataService = userDataService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("to,from,content")] Transfer transfer)
        {
            //Message message = new Message();
            //message.sent = false;
            //message.created = DateTime.Now;
            //message.content = transfer.content;
            //string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            //List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            //if (!allContactsId.Contains(transfer.from))
            //{
            //    return NotFound("Contact does not exist!");
            //}
            //MessageDB message = new MessageDB();
            //message.content = transfer.content;
            //message.created = DateTime.Now;
            //message.from = transfer.from;
            //message.to = transfer.to;
            //int messageId = messageDBModel.AddMessage(message, message.from, message.to);
            //int userContactId = 0;
            //List<UserContacts> users = userContactsModel.GetAllUsersContacts();
            //foreach (var user in users)
            //{
            //    if (user.userId == message.to && user.contactId == message.from)
            //    {
            //        user.lastMessageId = messageId;
            //        userContactId = user.id;
            //    }
            //}
            //chatMessagesModel.AddMessage(userContactId, messageId);
            //string userId = transfer.to;
            //string contactId = transfer.from;
            //int lastMessId = 0;
            //List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            //if (!allContactsId.Contains(contactId))
            //{
            //    return NotFound("Contact does not exist!");
            //}
            //List<UserContacts> allUserContacts = userContactsModel.GetAllUsersContacts();
            //foreach (var userContact in allUserContacts)
            //{
            //    if (userContact.userId == contactId && userContact.contactId == userId)
            //    {
            //        lastMessId = userContact.lastMessageId;
            //    }
            //}
            //UserContacts u = allUserContacts.Find(x => x.userId == userId && x.contactId == contactId);
            //foreach (var userContact in allUserContacts)
            //{
            //    if (userContact.userId == userId && userContact.contactId == contactId)
            //    {
            //        userContactsModel.UpdateContact(userContact.id, lastMessId);
            //        chatMessagesModel.AddMessage(userContact.id, lastMessId);
            //    }
            //}
            //userContactsModel.UpdateContact(u.id, lastMessId);
            await chatHub.SendMessage(transfer.content);
            return Created("Message sent succesfully!", transfer.content);












            // old method while using static DB

            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.username == transfer.to)
            //    {
            //        foreach (Chat chat in userChats.chats)
            //        {
            //           if (chat.contactUserName.id == transfer.from)
            //            {
            //                message.id = -1;
            //                chat.Messages.Add(message);
            //                chat.ContactUserName.last = message.content;
            //                chat.ContactUserName.lastdate = message.created;
            //                await chatHub.SendMessage(transfer.content);
            //                return Created("Message sent succesfully!", message);
            //            }
            //        }
            //    }
            //}
            //return NotFound("Contact does not exist!");
        }
    };
}