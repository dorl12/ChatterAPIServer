using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ChatterAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        //private readonly IUserDataService _userDataService;
        private IUserContactsModel userContactsModel = new UserContactsModel();
        private IContactModel contactModel = new ContactModel();

        //public ContactsController(IUserDataService userDataService)
        //{
        //    //_userDataService = userDataService;
        //}

        [HttpGet]
        public IActionResult Index()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type.EndsWith("UserId"))?.Value;
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            List<Contact> allContacts = new List<Contact>();
            Contact contact = new Contact();
            foreach(var contactId in allContactsId)
            {
                contact = contactModel.GetContact(contactId);
                allContacts.Add(contact);
            }
            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == userId)
            //    {
            //        foreach (Chat chat in userChats.Chats)
            //        {
            //            allContacts.Add(chat.ContactUserName);
            //        }
            //    }
            //}
            return Ok(allContacts);
        }

        [HttpGet("{id}")]
        public IActionResult Detailes(string? id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            if (!allContactsId.Contains(id))
            {
                return NotFound("Contact does not exist!");
            }
            //Chat toFind = new Chat();
            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == userId)
            //    {
            //        toFind = (userChats.Chats.Where(x => x.ContactUserName.id == id).FirstOrDefault());
            //        if (toFind == null)
            //        {
            //            return NotFound("Contact does not exist!");
            //        }
            //    }
            //}
            return Ok(contactModel.GetContact(id));
        }

        [HttpPost]
        public IActionResult Create([Bind("name,server,last,lastdate")] Contact contact)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            if (allContactsId.Contains(contact.id))
            {
                return BadRequest("Contact already exist!");
            }
            contact.last = "";
            contact.lastdate = new DateTime();
            userContactsModel.AddContactToUser(contact.id, userId);
            List<Contact> allContacts = contactModel.GetAllContacts();
            if (!allContacts.Contains(contact))
            {
                contactModel.AddContact(contact);
            }
            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == userId)
            //    {
            //        if (userChats.Chats.Where(x => x.ContactUserName.id == contact.id).FirstOrDefault() != null)
            //        {
            //            return BadRequest("Contact already exist!");
            //        }
            //        contact.last = "";
            //        contact.lastdate = new DateTime();
            //        userChats.Chats.Add(new Chat() { ContactUserName = contact, Messages = new List<Message>() });
            //        return Created("Contact Added", contact);
            //    }
            //}
            return Ok("");
        }

        [HttpPut("{id}")]
        public IActionResult Update([Bind("name,server")] Contact contact, string? id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            if (!allContactsId.Contains(id))
            {
                return NotFound("Contact does not exist!");
            }
            contactModel.UpdateContact(contact, id);
            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == userId)
            //    {
            //        Contact c = userChats.Chats.Where(x => x.ContactUserName.id == id).FirstOrDefault().ContactUserName;
            //        if (c == null)
            //        {
            //            return NotFound("Contact does not exist!");
            //        }
            //        c.name = contact.name;
            //        c.server = contact.server;
            //    }
            //}
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(string? id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            List<string> allContactsId = userContactsModel.GetAllUserContacts(userId);
            if (!allContactsId.Contains(id))
            {
                return NotFound("Contact does not exist!");
            }
            contactModel.DeleteContact(id);
            userContactsModel.DeleteContactFromUser(id, userId);
            //Chat toFind = new Chat();
            //foreach (UserChats userChats in _userDataService.GetAllUsersChats())
            //{
            //    if (userChats.Username == userId)
            //    {
            //        toFind = (userChats.Chats.Where(x => x.ContactUserName.id == id).FirstOrDefault());
            //        if (toFind == null)
            //        {
            //            return NotFound("Contact does not exist!");
            //        }
            //        userChats.Chats.Remove(toFind);
            //        break;
            //    }
            //}
            return NoContent();
        }
    }}