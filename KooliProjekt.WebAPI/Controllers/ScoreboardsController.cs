using KooliProjekt.Application.Features.Games.Queries;
using KooliProjekt.Application.Features.Scoreboards.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreboardsController : ControllerBase // 5. Pärineb baaskontrollerist
    {
        private readonly IMediator _mediator;

        public ScoreboardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 6. List meetod
        [HttpGet]
        public async Task<ActionResult<List<ScoreboardDto>>> Get([FromQuery] GetGamesListQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result); // 7. Küsib andmeid, mis on olemas või pole (tagastab listi)
        }

        // 6. Get meetod ühe kirje jaoks
        [HttpGet("{id}")]
        public async Task<ActionResult<ScoreboardDto>> Get(int id)
        {
            // 8. Kontrolli kontrolleris, et Id poleks null või sellest väiksem
            if (id <= 0)
            {
                return NotFound();
            }

            var query = new GetPredictionDetailsQuery { Id = id };
            var result = await _mediator.Send(query);

            // 9. Kui mediatori Send() tagastas nulli, tagasta NotFound
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result); // 7. & 10. Tagastab leitud kirje
        }
    }
}