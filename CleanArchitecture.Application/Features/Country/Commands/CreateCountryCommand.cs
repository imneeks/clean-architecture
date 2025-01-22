using CleanArchitecture.Shared;
using MediatR;

namespace CleanArchitecture.Application.Features.Country.Commands
{
    public record CreateCountryCommand(string Name, string CountryCode, string CountryDialCode) : IRequest<Result>, ICountryCommand;

}
