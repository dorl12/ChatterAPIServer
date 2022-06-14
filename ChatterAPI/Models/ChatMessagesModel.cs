using ChatterDB;

namespace ChatterAPI
{
    public interface IChatMessagesModel
    {
        List<ChatMessages> getAllChatMessages();
        void AddMessage(int userContactID, int messageID);
        void DeleteMessage(int id);
    }
    public class ChatMessagesModel : IChatMessagesModel
    {
        private static int idCounter = 0;

        // Get all chat messages
        public List<ChatMessages> getAllChatMessages()
        {
            using (var db = new UsersContext())
            {
                if (idCounter == 0)
                {
                    return new List<ChatMessages>();
                }
                return db.ChatMessages.ToList();
            }
        }


        // Add Message
        public void AddMessage(int userContactID, int messageID)
        {
            using (var db = new UsersContext())
            {
                ChatMessages chatMessages = new ChatMessages();
                chatMessages.id = ++idCounter;
                chatMessages.userContactId = userContactID;
                chatMessages.messageId = messageID;
                db.ChatMessages.Add(chatMessages);
                db.SaveChanges();
            }
        }

        // Delete Message
        public void DeleteMessage(int id)
        {
            using (var db = new UsersContext())
            {
                ChatMessages chat = db.ChatMessages.Find(id);
                db.ChatMessages.Remove(chat);
                db.SaveChanges();
            }
        }
    }
}



