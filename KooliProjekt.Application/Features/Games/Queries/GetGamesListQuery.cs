using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Games.Queries
{
    public class GetGamesListQuery : IRequest<List<GameDto>>
    {
    }

    public class GetGamesListQueryHandler : IRequestHandler<GetGamesListQuery, List<GameDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetGamesListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GameDto>> Handle(GetGamesListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Games
                .Select(g => new GameDto
                {
                    Id = g.Id
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class GameDto
    {
        public int Id { get; set; }
    }
}