using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Tournaments.Commands
{
    public class SaveTournamentCommandHandler : IRequestHandler<SaveTournamentCommand, OperationResult<int>>
    {
        private readonly ApplicationDbContext _context;

        public SaveTournamentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<int>> Handle(SaveTournamentCommand request, CancellationToken cancellationToken)
        {
            Tournament tournament;

            if (request.Id == 0)
            {
                tournament = new Tournament();
                _context.Tournaments.Add(tournament);
            }
            else
            {
                tournament = await _context.Tournaments.FindAsync(new object[] { request.Id }, cancellationToken);
                if (tournament == null)
                {
                    return OperationResult<int>.NotFound("Tournament not found");
                }
            }

            tournament.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult<int>.Success(tournament.Id);
        }
    }
}