using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Teams.Commands
{
    public class SaveTeamCommandHandler : IRequestHandler<SaveTeamCommand, OperationResult<int>>
    {
        private readonly ApplicationDbContext _context;

        public SaveTeamCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<int>> Handle(SaveTeamCommand request, CancellationToken cancellationToken)
        {
            Team team;

            if (request.Id == 0)
            {
                team = new Team();
                _context.Teams.Add(team);
            }
            else
            {
                team = await _context.Teams.FindAsync(new object[] { request.Id }, cancellationToken);
                if (team == null)
                {
                    return OperationResult<int>.NotFound("Team not found");
                }
            }

            team.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult<int>.Success(team.Id);
        }
    }
}