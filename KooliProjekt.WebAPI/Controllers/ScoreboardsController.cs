using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Features.Scoreboards.Queries;
using KooliProjekt.Application.Features.Scoreboards.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreboardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScoreboardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ScoreboardDto>>> Get([FromQuery] GetScoreboardsListQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            if (!result.Succeeded)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScoreboardDto>> Get(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var query = new GetScoreboardDetailsQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);

            if (!result.Succeeded)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveScoreboardCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteScoreboardCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Succeeded)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }
    }
}