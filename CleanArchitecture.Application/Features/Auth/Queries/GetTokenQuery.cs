using CleanArchitecture.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Queries
{
    public record GetTokenQuery(string UserName, string Password) : IRequest<Result>;    
}
