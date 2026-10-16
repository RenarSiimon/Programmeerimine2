using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Features.Prediction.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public PredictionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PredictionDto>>> Get([FromQuery] GetPredictionsListQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PredictionDto>> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var query = new GetPredictionDetailsQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}