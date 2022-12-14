using AddressBook.Contacts.DomainCore;
using System.Linq.Expressions;

namespace AddressBook.Contacts.Infrastructure.Repositories.Base
{
    public interface IRepository<T> where T : Entity, IAggregateRoot
    {
        public Task AddRangeAsync(List<T> entity);
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        public Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        public Task SaveChangesAsync();
        public Task MigrateDatabaseAsync();
    }
}
