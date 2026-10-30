using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Games.Commands
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteGameCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.FindAsync(new object[] { request.Id }, cancellationToken);
            if (game == null)
            {
                return OperationResult.NotFound("Game not found");
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}