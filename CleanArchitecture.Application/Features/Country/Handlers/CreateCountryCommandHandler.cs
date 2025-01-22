using CleanArchitecture.Application.Features.Country.Commands;
using CleanArchitecture.Application.Features.Shared;
using CleanArchitecture.Core.Entity;
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
    public class CreateCountryCommandHandler : BaseHandler, IRequestHandler<CreateCountryCommand, Result>
    {

        private readonly IUnitOfWork _unitOfWork;
        
        public CreateCountryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {

            Core.Entity.Country country = new Core.Entity.Country { CountryCode = request.CountryCode, CountryDialCode = request.CountryDialCode, Name = request.Name, CreatedAt = DateTime.UtcNow, CreatedBy = 1 };

            await _unitOfWork.CountryRepository.AddAsync(country);
            await _unitOfWork.CompleteAsync();

            return Result.Success(country.CountryId);
        }
    }
}
