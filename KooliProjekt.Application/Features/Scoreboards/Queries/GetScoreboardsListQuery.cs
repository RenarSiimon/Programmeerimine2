using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Scoreboards.Queries
{
    public class GetTournamentsListQuery : IRequest<List<ScoreboardDto>>
    {
    }

    public class GetScoreboardsListQueryHandler : IRequestHandler<GetTournamentsListQuery, List<ScoreboardDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetScoreboardsListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ScoreboardDto>> Handle(GetTournamentsListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Scoreboards
                .Select(s => new ScoreboardDto
                {
                    Id = s.Id,
                    Place = s.Place,
                    Points = s.Points
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class ScoreboardDto
    {
        public int Id { get; set; }
        public int Place { get; set; }
        public int Points { get; set; }
    }
}