using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Games.Queries
{
    public class GetGameDetailsQuery : IRequest<GameDto>
    {
        public int Id { get; set; }
    }

    public class GetGameDetailsQueryHandler : IRequestHandler<GetGameDetailsQuery, GameDto>
    {
        private readonly ApplicationDbContext _context;

        public GetGameDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GameDto> Handle(GetGameDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Games
                .Where(g => g.Id == request.Id)
                .Select(g => new GameDto
                {
                    Id = g.Id
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}