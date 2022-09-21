using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Domain.Sp;
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

        public async Task AddRangeAsync(List<Contact> entity)
        {
            await _context.Contacts.AddRangeAsync(entity);
            await SaveChangesAsync();
        }

        public async Task AddAsync(Contact entity)
        {
            await _context.Contacts.AddAsync(entity);
            await SaveChangesAsync();
        }

        public Task UpdateAsync(Contact entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task MigrateDatabaseAsync() => await _context.Database.MigrateAsync();

        public async Task<IEnumerable<Contact>> GetListAsync(Expression<Func<Contact, bool>> predicate)
        {
            return await _context.Set<Contact>().Where(predicate).Include(i => i.ContactInformation).ToListAsync();
        }

        public async Task<Contact> GetAsync(Expression<Func<Contact, bool>> predicate)
        {
            return await _context.Set<Contact>().Where(predicate).Include(i => i.ContactInformation).FirstOrDefaultAsync();
        }

        public Task DeleteAsync(Contact entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
