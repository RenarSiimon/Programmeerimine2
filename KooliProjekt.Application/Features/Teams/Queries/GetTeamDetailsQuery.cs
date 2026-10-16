using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Teams.Queries
{
    public class GetTeamsListQuery : IRequest<List<TeamDto>>
    {
    }

    public class GetTeamsListQueryHandler : IRequestHandler<GetTeamsListQuery, List<TeamDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetTeamsListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeamDto>> Handle(GetTeamsListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Select(t => new TeamDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}