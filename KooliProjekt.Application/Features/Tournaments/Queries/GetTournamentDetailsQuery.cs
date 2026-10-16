using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Tournaments.Queries
{
    public class GetTournamentDetailsQuery : IRequest<TournamentDto>
    {
        public int Id { get; set; }
    }

    public class GetTournamentDetailsQueryHandler : IRequestHandler<GetTournamentDetailsQuery, TournamentDto>
    {
        private readonly ApplicationDbContext _context;

        public GetTournamentDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TournamentDto> Handle(GetTournamentDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tournaments
                .Where(t => t.Id == request.Id)
                .Select(t => new TournamentDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}