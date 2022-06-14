using ChatterDB;

namespace ChatterAPI
{
    public interface IUserContactsModel
    {
        List<UserContacts> GetAllUsersContacts();
        List<string> GetAllUserContacts(string userId);
        void AddContactToUser(String contactId, string userId);
        void UpdateContact(int id, int lastMessageId);
        void DeleteContactFromUser(string id, string userId);
    }
    public class UserContactsModel : IUserContactsModel
    {
        private static int idCounter = 0;

        // Get all contacts of all users
        public List<UserContacts> GetAllUsersContacts()
        {
            using (var db = new UsersContext())
            {
                return db.UsersContacts.ToList();
            }
        }

        // Get all contacts of the connected User
        public List<string> GetAllUserContacts(string userId)
        {
            using (var db = new UsersContext())
            {
                if(idCounter == 0)
                {
                    return new List<string>();
                }
                var usersContactsList = db.UsersContacts.ToList();
                List<string> contacts = new List<string>();
                foreach(var contact in usersContactsList)
                {
                    if(contact.userId == userId)
                    {
                        contacts.Add(contact.contactId);
                    }
                }
                return contacts;
            }
        }

        // Add Contact to User
        public void AddContactToUser(string contactId, string userId)
        {
            using (var db = new UsersContext())
            {
                List<UserContacts> userContactsList = db.UsersContacts.ToList();
                foreach(var userContact in userContactsList)
                {
                    if(userContact.userId == userId && userContact.contactId == contactId)
                    {
                        return;
                    }
                }
                UserContacts userContacts = new UserContacts();
                userContacts.id = ++idCounter;
                userContacts.userId = userId;
                userContacts.contactId = contactId;
                db.UsersContacts.Add(userContacts);
                db.SaveChanges();
            }
        }

        // Update Contact
        public void UpdateContact(int id, int lastMessageId)
        {
            using (var db = new UsersContext())
            {
                UserContacts u = db.UsersContacts.Find(id);
                u.lastMessageId = lastMessageId;
                db.SaveChanges();
            }
        }

        // Delete Contact from User
        public void DeleteContactFromUser(string id, string userId)
        {
            using (var db = new UsersContext())
            {
                var usersContacts = db.UsersContacts.ToList();
                foreach (var userContacts in usersContacts)
                {
                    if (userContacts.userId == userId && userContacts.contactId == id)
                    {
                        usersContacts.Remove(userContacts);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}


