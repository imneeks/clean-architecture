using CleanArchitecture.Application.Features.Shared;
using CleanArchitecture.Application.Features.Country.Commands;
using FluentValidation;
using Microsoft.Extensions.Configuration;


namespace CleanArchitecture.Application.Features.Country.Validations
{
    public class UpdateCountryCommandValidator : CountryCommandBaseValidation<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator(IConfiguration configuration) : base(configuration)
        {
            RuleFor(x => x.CountryId)                
            .Required(GetRequiredMessage("Country Id"));
        }
    }   
}
