using System.Linq.Expressions;
 
namespace CleanArchitecture.Core.Interface
{
    namespace Core.Interfaces
    {
        public interface IGenericRepository<T> where T : class
        {
            Task<T> GetByIdAsync(object id); // Fetch by primary key
            Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken); // Fetch all records
            Task<List<TResult>> GetSelectedFieldsAsync<TResult>(Expression<Func<T, TResult>> selector);
            Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate); // Query with conditions
            Task AddAsync(T entity); // Add a new record
            void Update(T entity); // Update an existing record
            void Delete(T entity); // Delete a record
        }
    }
}
