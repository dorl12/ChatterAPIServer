using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ChatterAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/contacts/{contact}/messages")]
    public class MessagesController : ControllerBase
    {
        //private static int MessageID = 0;
        //private readonly IUserDataService _userDataService;
        private IUserContactsModel userContactsModel = new UserContactsModel();
        private IChatMessagesModel chatMessagesModel = new ChatMessagesModel();
        private ImessageDBModel messageDBModel = new MessageDBModel();
        private IContactModel contactModel = new ContactModel();

        //public MessagesController(IUserDataService userDataService)
        //{
        //    //_userDataService = userDataService;
        //}

        [HttpGet]
        public IActionResult Index(string contact)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            if (!allContactsId.Contains(contact))
            {
                return NotFound("Contact does not exist!");
            }
            List<UserContacts> allUserContacts = userContactsModel.GetAllUsersContacts();
            int userContactId = 0;
            foreach(var userContact in allUserContacts)
            {
                if(userContact.userId == userId && userContact.contactId == contact)
                {
                    userContactId = userContact.id;
                }
            }
            List<int> messagesID = new List<int>();
            List<ChatMessages> allChatMessages = chatMessagesModel.getAllChatMessages();
            foreach (var chatMessage in allChatMessages)
            {
                if(chatMessage.userContactId == userContactId)
                {
                    messagesID.Add(chatMessage.messageId);
                }
            }
            List <MessageDB> messageDBs = new List<MessageDB>();
            List<MessageDB> allMessages = messageDBModel.GetAllMessages();
            foreach(var m in allMessages)
            {
                if (messagesID.Contains(m.id))
                {
                    messageDBs.Add(m);
                }
            }
            List<Message> messages = new List<Message>();
            foreach(var messageDB in messageDBs)
            {
                Message message = new Message();
                message.id = messageDB.id;
                message.content = messageDB.content;
                message.created = messageDB.created;
                if(messageDB.from == userId)
                {
                    message.sent = true;
                }
                else
                {
                    message.sent = false;
                }
                messages.Add(message);
            }
            return Ok(messages);

            //foreach (var UserContact in userContactsModel.GetAllUserContacts)


            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == userId)
            //    {
            //        foreach (Chat chat in userChats.Chats)
            //        {
            //            if (chat.ContactUserName.id == contact)
            //            {
            //                return Ok(chat.Messages);
            //            }
            //        }
            //    }
            //}
            //return NotFound("Contact does not exist!");
        }

        [HttpGet("{m_id}")]
        public IActionResult Detailes(string contact, int m_id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            if (!allContactsId.Contains(contact))
            {
                return NotFound("Contact does not exist!");
            }
            List<UserContacts> allUserContacts = userContactsModel.GetAllUsersContacts();
            int userContactId = 0;
            foreach (var userContact in allUserContacts)
            {
                if (userContact.userId == userId && userContact.contactId == contact)
                {
                    userContactId = userContact.id;
                }
            }
            List<int> messagesID = new List<int>();
            List<ChatMessages> allChatMessages = chatMessagesModel.getAllChatMessages();
            foreach (var chatMessage in allChatMessages)
            {
                if (chatMessage.userContactId == userContactId)
                {
                    messagesID.Add(chatMessage.messageId);
                }
            }
            List<MessageDB> messageDBs = new List<MessageDB>();
            List<MessageDB> allMessages = messageDBModel.GetAllMessages();
            foreach (var m in allMessages)
            {
                if (messagesID.Contains(m.id))
                {
                    messageDBs.Add(m);
                }
            }
            MessageDB messageDB = messageDBs.Find(i => i.id == m_id);
            if (messageDB != null)
            {
                Message toFind = new Message();
                toFind.id = messageDB.id;
                toFind.content = messageDB.content;
                toFind.created = messageDB.created;
                if (messageDB.from == userId)
                {
                    toFind.sent = true;
                }
                else
                {
                    toFind.sent = false;
                }
                return Ok(toFind);
            }
            return NotFound("Message does not exist!");

            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == userId)
            //    {
            //        foreach (Chat chat in userChats.Chats)
            //        {
            //            if (chat.ContactUserName.id == contact)
            //            {
            //                if (chat.Messages.Where(x => x.id == m_id).FirstOrDefault() == null)
            //                {
            //                    return NotFound("Message does not exist!");
            //                }
            //                return Ok(chat.Messages.Where(x => x.id == m_id).FirstOrDefault());
            //            }
            //        }
            //    }
            //}
            //return NotFound("Contact does not exist!");
        }

        [HttpPost]
        public IActionResult Create([Bind("content")] Message message, string contact)
        {
            //message.sent = true;
            //message.created = DateTime.Now;
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            if (!allContactsId.Contains(contact))
            {
                return NotFound("Contact does not exist!");
            }
            MessageDB newMessage = new MessageDB();
            //newMessage.id = message.id;
            newMessage.content = message.content;
            newMessage.created = DateTime.Now;
            newMessage.from = userId;
            newMessage.to = contact;
            int newMessageId = messageDBModel.AddMessage(newMessage, contact, userId);
            int userContactId = 0;
            List<UserContacts> users = userContactsModel.GetAllUsersContacts();
            foreach(var user in users)
            {
                if(user.userId == userId && user.contactId == contact)
                {
                    //user.lastMessageId = newMessageId;
                    userContactsModel.UpdateContact(user.id, newMessageId);
                    userContactId = user.id;
                }
            }
            contactModel.UpdateLastMessage(contact, newMessage.content, newMessage.created);
            chatMessagesModel.AddMessage(userContactId, newMessageId);
            return Created("Message Added", message);

            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == userId)
            //    {
            //        foreach (Chat chat in userChats.Chats)
            //        {
            //            if (chat.ContactUserName.id == contact)
            //            {
            //                message.id = MessageID++;
            //                chat.Messages.Add(message);
            //                chat.ContactUserName.last = message.content;
            //                chat.ContactUserName.lastdate = message.created;
            //                return Created("Message Added", message);
            //            }
            //        }
            //    }
            //}
            //return NotFound("Contact does not exist!");
        }

        [HttpPut("{m_id}")]
        public IActionResult Update([Bind("content")] Message message, string contact, int? m_id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            if (!allContactsId.Contains(contact))
            {
                return NotFound("Contact does not exist!");
            }
            List<MessageDB> messages = messageDBModel.GetAllMessages();
            foreach(var mess in messages)
            {
                if(mess.id == m_id)
                {
                    mess.content = message.content;
                    return NoContent();
                }
            }
            return NotFound("Message does not exist!");

            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == userId)
            //    {
            //        foreach (Chat chat in userChats.Chats)
            //        {
            //            if (chat.ContactUserName.id == contact)
            //            {
            //                foreach (Message mes in chat.Messages)
            //                {
            //                    if (mes.id == m_id)
            //                    {
            //                        mes.content = message.content;
            //                        NoContent();
            //                    }
            //                }
            //                return NotFound("Message does not exist!");
            //            }
            //        }
            //    }
            //}
            //return NotFound("Contact does not exist!");
        }

        [HttpDelete("{m_id}")]
        public IActionResult Delete(string contact, int m_id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            if (!allContactsId.Contains(contact))
            {
                return NotFound("Contact does not exist!");
            }
            bool messageHasDeleted = false;
            List<MessageDB> messages = messageDBModel.GetAllMessages();
            foreach(var mess in messages)
            {
                if(mess.id == m_id)
                {
                    messageDBModel.DeleteMessage(m_id);
                    messageHasDeleted = true;
                }
            }
            List<ChatMessages> chatMessages = chatMessagesModel.getAllChatMessages();
            foreach(var chat in chatMessages)
            {
                if(chat.messageId == m_id)
                {
                    chatMessagesModel.DeleteMessage(chat.id);
                }
            }
            if(!messageHasDeleted)
            {
                return NotFound("Message does not exist!");
            }
            return NoContent();
            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == userId)
            //    {
            //        foreach (Chat chat in userChats.Chats)
            //        {
            //            if (chat.ContactUserName.id == contact)
            //            {
            //                foreach (Message mes in chat.Messages)
            //                {
            //                    if (mes.id == m_id)
            //                    {
            //                        chat.Messages.Remove(mes);
            //                        return NoContent();
            //                    }
            //                }
            //                return NotFound("Message does not exist!");
            //            }
            //        }
            //    }
            //}
            //return NotFound("Contact does not exist!");
        }
    };
}