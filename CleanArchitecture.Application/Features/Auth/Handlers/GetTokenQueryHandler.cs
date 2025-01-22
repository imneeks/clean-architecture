using CleanArchitecture.Application.Features.Auth.Queries;
using CleanArchitecture.Core.Interface;
using CleanArchitecture.Shared;
using MediatR;
using CleanArchitecture.Authentication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entity;
using CleanArchitecture.Application.Features.Shared;

namespace CleanArchitecture.Application.Features.Auth.Handlers
{
    public class GetTokenQueryHandler : BaseHandler, IRequestHandler<GetTokenQuery, Result>
    {
        private readonly IUserRepository _user;
        private readonly ITokenService _tokenService;
        public GetTokenQueryHandler(IUserRepository user, ITokenService tokenService) {
            _user = user;
            _tokenService = tokenService;
        }


        public async Task<Result> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            User user = await _user.ValidateUser(request.UserName, request.Password);
            if (user != null) {

                var roles = await _user.GetRoles(user.UserId);
                var token = _tokenService.GenerateToken(user.UserId.ToString(),user.UserName, roles);

                return Result.Success(token);
            }

            throw new ItemNotFoundException("InvalidCredentials".TransformLanguage());
        }
    }
}
