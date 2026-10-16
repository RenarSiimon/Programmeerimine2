using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Tournaments.Queries
{
    public class GetTournamentsListQuery : IRequest<List<TournamentDto>>
    {
    }

    public class GetTournamentsListQueryHandler : IRequestHandler<GetTournamentsListQuery, List<TournamentDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetTournamentsListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TournamentDto>> Handle(GetTournamentsListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tournaments
                .Select(t => new TournamentDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class TournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}