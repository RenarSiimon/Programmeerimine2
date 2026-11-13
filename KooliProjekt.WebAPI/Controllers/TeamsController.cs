using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Features.Teams.Queries;
using KooliProjekt.Application.Features.Teams.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<TeamDto>>> Get([FromQuery] GetTeamsListQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            if (!result.Succeeded)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> Get(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var query = new GetTeamDetailsQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);

            if (!result.Succeeded)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveTeamCommand command, CancellationToken cancellationToken)
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
            var command = new DeleteTeamCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Succeeded)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }

        [HttpPost("{id}/files")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> AddFile(int id, IFormFile file, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddFileCommand { TeamId = id, File = file }, cancellationToken);
            if (!result.Succeeded)
            {
                return result.Message == "Team not found."
                    ? NotFound(result.Message)
                    : BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}/files")]
        public async Task<IActionResult> ListFiles(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ListFilesQuery { TeamId = id }, cancellationToken);
            if (!result.Succeeded)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        [HttpGet("{id}/files/{fileName}")]
        public async Task<IActionResult> GetFile(int id, string fileName, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetFileQuery { TeamId = id, FileName = fileName }, cancellationToken);
            if (!result.Succeeded)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("{id}/files/{fileName}")]
        public async Task<IActionResult> DeleteFile(int id, string fileName, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteFileCommand { TeamId = id, FileName = fileName }, cancellationToken);
            if (!result.Succeeded)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }
    }
}