using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Games.Commands
{
    public class SaveGameCommandHandler : IRequestHandler<SaveGameCommand, OperationResult<int>>
    {
        private readonly ApplicationDbContext _context;

        public SaveGameCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<int>> Handle(SaveGameCommand request, CancellationToken cancellationToken)
        {
            Game game;

            if (request.Id == 0)
            {
                game = new Game();
                _context.Games.Add(game);
            }
            else
            {
                game = await _context.Games.FindAsync(new object[] { request.Id }, cancellationToken);
                if (game == null)
                {
                    return OperationResult<int>.NotFound("Game not found");
                }
            }

            game.StartTime = request.StartTime;
            game.HomeTeamGoals = request.HomeTeamGoals;
            game.AwayTeamGoals = request.AwayTeamGoals;

            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult<int>.Success(game.Id);
        }
    }
}