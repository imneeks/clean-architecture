using CleanArchitecture.Core.Entity;
using CleanArchitecture.Core.Interface;
using CleanArchitecture.Infrastructure.Persistence.Data;
using CleanArchitecture.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
 

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context) {
        }

        public async Task<Country> GetByCountryCodeAsync(string countryCode)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CountryCode == countryCode);
        }

        public async Task<string> StaticVal()
        {
            await Task.Delay(2000);

            return "Hello World";
        }
    }
}
