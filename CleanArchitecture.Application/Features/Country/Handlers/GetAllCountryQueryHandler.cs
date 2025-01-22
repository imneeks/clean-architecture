using CleanArchitecture.Application.Features.Country.Queries;
using CleanArchitecture.Application.Features.Shared;
using CleanArchitecture.Core.Interface;
using CleanArchitecture.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Country.Handlers
{
    public class GetAllCountryQueryHandler : BaseHandler, IRequestHandler<GetAllCountryQuery, Result>
    {
        private readonly ICountryRepository _country;

        public GetAllCountryQueryHandler(ICountryRepository country)
        {
            _country = country;
        }

        public async Task<Result> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)   
        {
            var countries = await   _country.GetSelectedFieldsAsync(c => new
            {
                c.CountryId,
                c.Name,
                c.CountryDialCode
            });

            return Result.Success(countries);
        }
    }
}
