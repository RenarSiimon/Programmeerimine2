using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Scoreboards.Queries
{
    public class GetScoreboardDetailsQuery : IRequest<OperationResult<ScoreboardDto>>
    {
        public int Id { get; set; }
    }

    public class GetScoreboardDetailsQueryHandler : IRequestHandler<GetScoreboardDetailsQuery, OperationResult<ScoreboardDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetScoreboardDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<ScoreboardDto>> Handle(GetScoreboardDetailsQuery request, CancellationToken cancellationToken)
        {
            var scoreboard = await _context.Scoreboards
                .Where(x => x.Id == request.Id)
                .Select(x => new ScoreboardDto
                {
                    Id = x.Id,
                    Place = x.Place,
                    Points = x.Points
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (scoreboard == null)
            {
                return OperationResult<ScoreboardDto>.NotFound("Scoreboard not found");
            }

            return OperationResult<ScoreboardDto>.Success(scoreboard);
        }
    }
}