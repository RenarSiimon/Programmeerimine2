using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Teams.Queries
{
    public class GetTeamDetailsQuery : IRequest<TeamDto>
    {
        public int Id { get; set; }
    }

    public class GetTeamDetailsQueryHandler : IRequestHandler<GetTeamDetailsQuery, TeamDto>
    {
        private readonly ApplicationDbContext _context;

        public GetTeamDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TeamDto> Handle(GetTeamDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Where(t => t.Id == request.Id)
                .Select(t => new TeamDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}