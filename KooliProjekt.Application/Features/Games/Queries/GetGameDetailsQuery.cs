using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.Application.Infrastructure.Paging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;

namespace KooliProjekt.Application.Features.Games.Queries
{
    public class GetGameDetailsQuery : IRequest<OperationResult<GameDto>>
    {
        public int Id { get; set; }
    }

    public class GetGameDetailsQueryHandler : IRequestHandler<GetGameDetailsQuery, OperationResult<GameDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetGameDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<GameDto>> Handle(GetGameDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Games
                .Where(g => g.Id == request.Id)
                .Select(g => new GameDto
                {
                    Id = g.Id
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return OperationResult<GameDto>.NotFound("Game not found.");
            }

            return OperationResult<GameDto>.Success(entity);
        }
    }
}