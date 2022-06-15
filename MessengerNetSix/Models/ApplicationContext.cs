using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MessengerNetSix.Models;

namespace MessengerNetSix.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<MessengerNetSix.Models.Contact> Contacts { get; set; }
        public DbSet<MessengerNetSix.Models.Message> Messages { get; set; }
        public DbSet<MessengerNetSix.Models.ChatList> ChatList { get; set; }
        public DbSet<MessengerNetSix.Models.ChatMember> ChatMembers { get; set; }
    }
}