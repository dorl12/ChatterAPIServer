using ChatterAPI;
using Microsoft.EntityFrameworkCore;

namespace ChatterDB
{
    public class UsersContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=ChatterDB;user=yoyo;password=password";
        //private const string connectionString = "server=localhost;port=3306;database=ChatterDB;user=root;password=geeksRules22";

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
            modelBuilder.Entity<MessageDB>().HasKey(e => e.id);
            modelBuilder.Entity<User>().HasKey(e => e.id);
            modelBuilder.Entity<UserContacts>().HasKey(e => e.id);
            modelBuilder.Entity<ChatMessages>().HasKey(e => e.id);
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<MessageDB> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserContacts> UsersContacts { get; set; }
        public DbSet<ChatMessages> ChatMessages { get; set; }

    }
}
