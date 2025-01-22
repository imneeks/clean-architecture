using CleanArchitecture.Application.Features.Shared;
using CleanArchitecture.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Country.Validations
{
    public abstract class CountryCommandBaseValidation<T> : BaseValidator<T>
      where T : class, ICountryCommand
    {
        public CountryCommandBaseValidation(IConfiguration configuration)            
        {
            RuleFor(x => x.Name)
            .Required(GetRequiredMessage("Country Name"))
            .MaxLength(100, GetMaxLengthMessage("Country Name", 100));

            RuleFor(x => x.CountryCode)
            .Required(GetRequiredMessage("Country Code"))
            .MaxLength(2, GetMaxLengthMessage("Country Code", 2));

            RuleFor(x => x.CountryDialCode)
               .Required(GetRequiredMessage("Country Dial Code"))
               .MaxLength(3, GetMaxLengthMessage("Country Dial Code", 3));
        }
    }
}
