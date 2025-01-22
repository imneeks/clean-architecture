using CleanArchitecture.Core.Entity;
using CleanArchitecture.Core.Interface.Core.Interfaces;
using CleanArchitecture.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interface
{
    public interface ICountryRepository : IGenericRepository<Country>
    { 
        Task<Country> GetByCountryCodeAsync(string countryCode);

        Task<string> StaticVal();
    }
}
