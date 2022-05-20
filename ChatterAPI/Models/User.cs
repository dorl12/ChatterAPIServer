namespace ChatterAPI
{
    public class User
    {
        //TO DO Valid Check
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
