using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.Application.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Teams.Queries
{
    public class GetTeamDetailsQuery : IRequest<OperationResult<TeamDto>>
    {
        public int Id { get; set; }
    }

    public class GetTeamDetailsQueryHandler : IRequestHandler<GetTeamDetailsQuery, OperationResult<TeamDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetTeamDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<TeamDto>> Handle(GetTeamDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Teams
                .Where(t => t.Id == request.Id)
                .Select(t => new TeamDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return OperationResult<TeamDto>.NotFound("Team not found.");
            }

            return OperationResult<TeamDto>.Success(entity);
        }
    }
}