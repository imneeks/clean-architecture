using CleanArchitecture.Application.Features.Country.Commands;
using CleanArchitecture.Application.Features.Shared;
using CleanArchitecture.Core.Interface;
using CleanArchitecture.Shared;
using MediatR;

namespace CleanArchitecture.Application.Features.Country.Handlers
{
    public class DeleteCountryCommandHandler : BaseHandler, IRequestHandler<DeleteCountryCommand, Result>
    {
        public readonly IUnitOfWork _unitOfWork;
        public DeleteCountryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _unitOfWork.CountryRepository.GetByIdAsync(request.CountryId);

            if (country == null)
            {
                throw new ItemNotFoundException(NotFound("Country"));
            }
            
            
            _unitOfWork.CountryRepository.Update(country);

            await _unitOfWork.CompleteAsync();

            return Result.Success();
        }
    }
}
