using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Teams.Commands
{
    public class DeleteTeamCommandHandlers : IRequestHandler<DeleteTeamCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteTeamCommandHandlers(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _context.Teams.FindAsync(new object[] { request.Id }, cancellationToken);
            if (team == null)
            {
                return OperationResult.NotFound("Team not found");
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}