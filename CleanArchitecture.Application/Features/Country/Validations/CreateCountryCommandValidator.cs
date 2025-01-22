using CleanArchitecture.Application.Features.Country.Commands;

using Microsoft.Extensions.Configuration;


namespace CleanArchitecture.Application.Features.Country.Validations
{
    public class CreateCountryCommandValidator : CountryCommandBaseValidation<CreateCountryCommand>
    {
        public CreateCountryCommandValidator(IConfiguration configuration) : base(configuration)
        {
            
        }
    }
}
