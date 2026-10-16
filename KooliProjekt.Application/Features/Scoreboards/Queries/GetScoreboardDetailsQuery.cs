using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Scoreboards.Queries
{
    public class GetPredictionDetailsQuery : IRequest<ScoreboardDto>
    {
        public int Id { get; set; }
    }

    public class GetScoreboardDetailsQueryHandler : IRequestHandler<GetPredictionDetailsQuery, ScoreboardDto>
    {
        private readonly ApplicationDbContext _context;

        public GetScoreboardDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ScoreboardDto> Handle(GetPredictionDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Scoreboards
                .Where(s => s.Id == request.Id)
                .Select(s => new ScoreboardDto
                {
                    Id = s.Id,
                    Place = s.Place,
                    Points = s.Points
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}