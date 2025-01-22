using CleanArchitecture.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Country.Queries
{
    public record GetCountryByIdQuery(int CountryId) : IRequest<Result>;
}
