using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Scoreboards.Commands
{
    public class DeleteScoreboardCommandHandler : IRequestHandler<DeleteScoreboardCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteScoreboardCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> Handle(DeleteScoreboardCommand request, CancellationToken cancellationToken)
        {
            var scoreboard = await _context.Scoreboards.FindAsync(new object[] { request.Id }, cancellationToken);
            if (scoreboard == null)
            {
                return OperationResult.NotFound("Scoreboard not found");
            }

            _context.Scoreboards.Remove(scoreboard);
            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}