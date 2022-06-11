using ChatterDB;

namespace ChatterAPI
{
    public interface IContactModel
    {
        List<Contact> GetAllContacts();
        Contact GetContact(string id);
        void AddContact(Contact contact);
        void UpdateContact(Contact contact, string id);
        void DeleteContact(string id);
    }
    public class ContactModel : IContactModel
    {
        // Get all Contacts
        public List<Contact> GetAllContacts()
        {
            using (var db = new UsersContext())
            {
                var contacts = db.Contacts.ToList();
                return contacts;
            }
        }

        // Get Contact
        public Contact GetContact(string id)
        {
            using (var db = new UsersContext())
            {
                Contact contact = db.Contacts.Find(id);
                return contact;
            }
        }

        // Add Contact
        public void AddContact(Contact contact)
        {
            using (var db = new UsersContext())
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
        }

        // Update Contact
        public void UpdateContact(Contact contact, string id)
        {
            using (var db = new UsersContext())
            {
                Contact c = db.Contacts.Find(id);
                c.name = contact.name;
                c.server = contact.server;
            }
        }

        // Delete Contact
        public void DeleteContact(string id)
        {
            using (var db = new UsersContext())
            {
                Contact c = db.Contacts.Find(id);
                db.Contacts.Remove(c);
                db.SaveChanges();
            }
        }
    }
}


