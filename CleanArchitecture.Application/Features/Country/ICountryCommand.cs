using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Country
{
    public interface ICountryCommand
    {   
        public string Name { get; }

        public string CountryCode { get; }

        public string CountryDialCode { get; }
    }
}
