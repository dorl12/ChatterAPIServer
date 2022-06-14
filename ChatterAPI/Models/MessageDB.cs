namespace ChatterAPI
{
    public class MessageDB
    {
        public int id { get; set; }
        public string? content { get; set; }
        public DateTime created { get; set; }
        public string from { get; set; }
        public string to { get; set; }

    }
}
