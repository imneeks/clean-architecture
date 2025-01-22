using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interface
{
    public interface IUnitOfWork
    {
        ICountryRepository CountryRepository { get; }
        Task<int> CompleteAsync();
    }
}
