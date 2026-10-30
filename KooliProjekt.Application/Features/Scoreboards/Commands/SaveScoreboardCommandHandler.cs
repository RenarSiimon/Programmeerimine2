using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Scoreboards.Commands
{
    public class SaveScoreboardCommandHandler : IRequestHandler<SaveScoreboardCommand, OperationResult<int>>
    {
        private readonly ApplicationDbContext _context;

        public SaveScoreboardCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<int>> Handle(SaveScoreboardCommand request, CancellationToken cancellationToken)
        {
            Scoreboard scoreboard;

            if (request.Id == 0)
            {
                scoreboard = new Scoreboard();
                _context.Scoreboards.Add(scoreboard);
            }
            else
            {
                scoreboard = await _context.Scoreboards.FindAsync(new object[] { request.Id }, cancellationToken);
                if (scoreboard == null)
                {
                    return OperationResult<int>.NotFound("Scoreboard not found");
                }
            }

            scoreboard.Place = request.Place;
            scoreboard.Points = request.Points;

            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult<int>.Success(scoreboard.Id);
        }
    }
}