using ChatterDB;

namespace ChatterAPI
{
    public interface ImessageDBModel
    {
        List<MessageDB> GetAllMessages();
        MessageDB GetMessage(int id);
        int AddMessage(MessageDB message, string contact, string userId);
        void DeleteMessage(int id);
    }
    public class MessageDBModel : ImessageDBModel
    {
        private static int idCounter = 0;

        public MessageDBModel()
        {
            using (var db = new UsersContext())
            {
                List<MessageDB> lst = db.Messages.ToList();
                if (lst.Count > 0)
                {
                    idCounter = lst.Count;
                }
            }
        }

        // Get all messages
        public List<MessageDB> GetAllMessages()
        {
            using (var db = new UsersContext())
            {
                return db.Messages.ToList();
            }
        }

        // Get Message
        public MessageDB GetMessage(int id)
        {
            using (var db = new UsersContext())
            {
                MessageDB message = db.Messages.Find(id);
                return message;
            }
        }

        // Add Message
        public int AddMessage(MessageDB message, string contact, string userId)
        {
            using (var db = new UsersContext())
            {
                //db.Messages.Add(message);
                //MessageDB newMessage = new MessageDB();
                message.id = ++idCounter;
                //newMessage.content = message.content;
                //newMessage.created = message.created;
                //newMessage.from = userId;
                //newMessage.to = contact;
                db.Messages.Add(message);
                db.SaveChanges();
                return idCounter;
            }
        }

        // Delete Message
        public void DeleteMessage(int id)
        {
            using (var db = new UsersContext())
            {
                MessageDB m = db.Messages.Find(id);
                db.Messages.Remove(m);
                db.SaveChanges();
            }
        }
    }
}


