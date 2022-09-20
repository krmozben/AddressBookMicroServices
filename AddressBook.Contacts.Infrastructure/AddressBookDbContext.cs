using AddressBook.Contacts.Domain.ContactsAggregate;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Contacts.Infrastructure
{
    public class AddressBookDbContext : DbContext
    {
        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options) : base(options)
        {
        }


        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().OwnsOne(o => o.Firm, on => { on.Property(p => p.Name).HasColumnName("Firm"); });

            base.OnModelCreating(modelBuilder);
        }
    }
}
