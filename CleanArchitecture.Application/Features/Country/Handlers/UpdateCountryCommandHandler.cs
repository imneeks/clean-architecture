using CleanArchitecture.Application.Features.Country.Commands;
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
    public class UpdateCountryCommandHandler : BaseHandler, IRequestHandler<UpdateCountryCommand, Result>
    {

        private readonly ICountryRepository _repository;
      
        public UpdateCountryCommandHandler(ICountryRepository repository)
        {
            _repository = repository;           
        }
 
        public Task<Result> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            return null;

        }
    }
}
