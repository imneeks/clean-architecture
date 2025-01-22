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
    public class GetCountryByIdQueryHandler : BaseHandler, IRequestHandler<GetCountryByIdQuery, Result>
    {
        private readonly ICountryRepository _country;

        public GetCountryByIdQueryHandler(ICountryRepository country)
        {
            _country = country;
        }

        public async Task<Result> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var countries = await _country.GetByIdAsync(request.CountryId);

            if(countries is null)
            {
                throw new ItemNotFoundException(NotFound("Country"));
            }
            //    .GetSelectedFieldsAsync(c => new
            //{
            //    Id = c.CountryId,
            //    c.Name,
            //    c.CountryDialCode
            //});


            return Result.Success(countries);
        }
    }
}
