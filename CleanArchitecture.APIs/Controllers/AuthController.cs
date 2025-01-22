using CleanArchitecture.Application.Features.Auth.Queries;
using CleanArchitecture.Application.Features.Country.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.APIs.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) {
            _mediator = mediator;
        }


        /// <summary>
        /// To Validate User
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Guid>> Login([FromBody] GetTokenQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
