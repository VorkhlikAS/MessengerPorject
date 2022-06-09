using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MessengerPAPS.Models;

namespace MessengerPAPS.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<MessengerPAPS.Models.Contact> Contacts { get; set; }
    }
}