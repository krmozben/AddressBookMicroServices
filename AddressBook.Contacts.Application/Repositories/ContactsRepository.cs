using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AddressBook.Contacts.Application.Repositories
{
    public class ContactsRepository : IRepository<Contact>
    {
        private readonly AddressBookDbContext _context;

        public ContactsRepository(AddressBookDbContext context) => _context = context;


        public Task AddAsync(Contact entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetAsync(Expression<Func<Contact, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Contact entity)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task MigrateDatabaseAsync() => await _context.Database.MigrateAsync();
    }
}
