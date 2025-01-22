using CleanArchitecture.Application.Features.Country.Commands;
using CleanArchitecture.Application.Features.Country.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;


        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("select")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _mediator.Send(new GetAllCountryQuery());
            return Ok(countries);
        }

        [HttpGet]
        [Route("select/{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var countries = await _mediator.Send(new GetCountryByIdQuery(id));
            return Ok(countries);
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<ActionResult<Guid>> CreateCountry([FromBody] CreateCountryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCountryById), new { id = result }, result);
        }

        [HttpPost]
        [Route("update")]
        [Authorize]
        public async Task<ActionResult<Guid>> UpdateCountry([FromBody] UpdateCountryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCountryById), new { id = result }, result);
        }
    }   
}
