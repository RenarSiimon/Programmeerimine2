using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Tournaments.Commands
{
    public class DeleteTournamentCommandHandler : IRequestHandler<DeleteTournamentCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteTournamentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
        {
            var tournament = await _context.Tournaments.FindAsync(new object[] { request.Id }, cancellationToken);
            if (tournament == null)
            {
                return OperationResult.NotFound("Tournament not found");
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}