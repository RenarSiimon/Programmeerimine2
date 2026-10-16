using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Features.Tournaments.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public TournamentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TournamentDto>>> Get([FromQuery] GetTournamentsListQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var query = new GetTournamentDetailsQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}