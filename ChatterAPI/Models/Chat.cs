namespace ChatterAPI
{
    public class Chat
    {
        public Contact contactUserName { get; set; }
        public List<MessageEntity>? messages { get; set; }
    }
}
