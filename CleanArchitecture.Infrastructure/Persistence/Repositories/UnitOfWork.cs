using CleanArchitecture.Core.Interface;
using CleanArchitecture.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICountryRepository _countries;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;         
        }

        

        public ICountryRepository CountryRepository => _countries ??= new CountryRepository(_context);       

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
