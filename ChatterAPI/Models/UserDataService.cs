namespace ChatterAPI
{
    public class UserDataService
    {
        public static List<User> _users = new List<User>(){
            new User() { Id = "yotam", Name = "Yotam Levin", Password = "yotam123", Image = "img1.jpg", Contacts = { } },
            new User() { Id = "or", Name = "Or Drukman", Password = "or123", Image = "img1.jpg", Contacts = { } },
            new User() { Id = "dor", Name = "Dor Levi", Password = "dor123", Image = "img1.jpg", Contacts = { } }
        };


        //public static List<Chat> _chats = new List<Chat>(){
        //    new Chat() {Username = "or", Messages = new List<Message>()
        //        {new Message() { id = 22, content = "Hi, how are you doing?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = false},
        //         new Message() { id = 18, content = "I'm good, wanna play volleyball?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = true}}},
        //    new Chat() {Username = "dor", Messages = new List<Message>()
        //        {new Message() { id = 8, content = "What's up?? how are you doing?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = false},
        //         new Message() { id = 928, content = "I'm good!!!!!!!!", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = true}}}
        //};

        public static List<UserChats> _AllUsersChats = new List<UserChats>(){
            new UserChats() { Username = "or", Chats = new List<Chat>(){
                            new Chat() {
                                ContactUserName = new Contact() { id = "yotam", name = "Yotam Levin", server = "localhost:7267", last = "I'm good, wanna play volleyball?", lastdate = new DateTime(2008, 5, 1, 8, 30, 52) }, 
                                Messages = new List<Message>()
                                        {new Message() { id = 18, content = "Hi, I am Yotam how are you doing?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = false},
                                         new Message() { id = 22, content = "I'm good, wanna play volleyball?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = true}}},
                            new Chat() {
                                ContactUserName = new Contact() { id = "dor", name = "Dor Levi", server = "localhost:7267", last = "I'm good!!!!!!!!", lastdate = new DateTime(2008, 5, 1, 8, 30, 52) },
                                Messages = new List<Message>()
                                        {new Message() { id = 8, content = "What's up?? I am Dor how are you doing?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = false},
                                         new Message() { id = 928, content = "I'm good!!!!!!!!", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = true}}}}},
            new UserChats() { Username = "yotam", Chats = new List<Chat>(){
                            new Chat() {
                                ContactUserName = new Contact() { id = "or", name = "Or Drukman", server = "localhost:7267", last = "I'm good, wanna play volleyball?", lastdate = new DateTime(2008, 5, 1, 8, 30, 52) },
                                Messages = new List<Message>()
                                        {new Message() { id = 13, content = "Hi, how are you doing?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = false},
                                         new Message() { id = 9, content = "I'm good, wanna play volleyball?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = true}}},
                            new Chat() {
                                ContactUserName = new Contact() { id = "dor", name = "Dor Levi", server = "localhost:7267", last = "I'm good!!!!!!!!", lastdate = new DateTime(2008, 5, 1, 8, 30, 52) },
                                Messages = new List<Message>()
                                        {new Message() { id = 15, content = "What's up?? how are you doing?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = false},
                                         new Message() { id = 200, content = "I'm good!!!!!!!!", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = true}}}}},
            new UserChats() { Username = "dor", Chats = new List<Chat>(){
                            new Chat() {
                                ContactUserName = new Contact() { id = "or", name = "Or Drukman", server = "localhost:7267", last = "I'm good, wanna play volleyball?", lastdate = new DateTime(2008, 5, 1, 8, 30, 52) },
                                Messages = new List<Message>()
                                        {new Message() { id = 18, content = "Hi, I am Yotam how are you doing?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = true},
                                         new Message() { id = 22, content = "I'm good, wanna play volleyball?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = false}}},
                            new Chat() {
                                ContactUserName = new Contact() { id = "yotam", name = "Yotam Levin", server = "localhost:7267", last = "I'm good, wanna play volleyball?", lastdate = new DateTime(2008, 5, 1, 8, 30, 52) },
                                Messages = new List<Message>()
                                        {new Message() { id = 15, content = "What's up?? how are you doing?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = true},
                                         new Message() { id = 200, content = "I'm good!!!!!!!!", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = false}}}}}

    };

        
        static UserDataService()
        {
            //UserDataService._chats = new List<Chat>();
            //UserDataService._chats.Add(new Chat() {Username = "or", Messages = new List<Message>() {new Message() { id = 1, content = "Hi, how are you doing?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = false},
            // new Message() { id = 18, content = "I'm good, wanna play volleyball?", created = new DateTime(2008, 5, 1, 8, 30, 52), sent = true}}});
            //UserDataService._contacts = new List<Contact>();
            //UserDataService._contacts.Add(new Contact() { id = "or", name = "Or Drukman", server = "localhost:7265", last = "text", lastdate = new DateTime(2008, 5, 1, 8, 30, 52) });
        }

    }
}