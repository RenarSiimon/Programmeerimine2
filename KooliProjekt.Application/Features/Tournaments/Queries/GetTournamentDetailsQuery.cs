using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;

namespace KooliProjekt.Application.Features.Tournaments.Queries
{
    public class GetTournamentDetailsQuery : IRequest<OperationResult<TournamentDto>>
    {
        public int Id { get; set; }
    }

    public class GetTournamentDetailsQueryHandler : IRequestHandler<GetTournamentDetailsQuery, OperationResult<TournamentDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetTournamentDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<TournamentDto>> Handle(GetTournamentDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tournaments
                .Where(t => t.Id == request.Id)
                .Select(t => new TournamentDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return OperationResult<TournamentDto>.NotFound("Tournament not found.");
            }

            return OperationResult<TournamentDto>.Success(entity);
        }
    }
}