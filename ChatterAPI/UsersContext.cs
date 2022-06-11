using ChatterAPI;
using Microsoft.EntityFrameworkCore;

namespace ChatterDB
{
    public class UsersContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=ChatterDB;user=root;password=geeksRules22";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
            //modelBuilder.Entity<Chat>().HasKey(e => e.ContactUserName);
            modelBuilder.Entity<Contact>().HasKey(e => e.id);
            modelBuilder.Entity<Message>().HasKey(e => e.id);
            modelBuilder.Entity<User>().HasKey(e => e.Id);
        }

        //public DbSet<Chat> Chats { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
