using CleanArchitecture.Application.Features.Auth.Queries;
using CleanArchitecture.Application.Features.Country;
using CleanArchitecture.Application.Features.Shared;
using CleanArchitecture.Application.Resources;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Application.Features.Auth.Validations
{

    public class GetTokenValidation : BaseValidator<GetTokenQuery>
    {
        public GetTokenValidation(IConfiguration configuration)            
        {
            RuleFor(x => x.UserName).Required(GetRequiredMessage("Username")).MaxLength(15, GetMaxLengthMessage("Username", 15));
            RuleFor(x => x.Password).Required(GetRequiredMessage("Password")).MaxLength(15, GetMaxLengthMessage("Password", 15));
        }
    }
}
