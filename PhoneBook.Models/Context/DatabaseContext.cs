using Microsoft.EntityFrameworkCore;
using PhoneBook.Models.Entities;

namespace PhoneBook.Models.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<ContactDetail> Contacts { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().ToTable(nameof(ApplicationUser));

            modelBuilder.Entity<Person>().ToTable(nameof(Person));

            modelBuilder.Entity<ContactDetail>().ToTable(nameof(ContactDetail));

        }
    }
}
