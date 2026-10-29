using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.Application.Infrastructure.Paging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Features.Prediction.Queries;
using KooliProjekt.Application.Features.Teams.Queries;

namespace KooliProjekt.Application.Features.Games.Queries
{
    public class GetGamesListQuery : IRequest<OperationResult<PagedResult<GameDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetGamesListQueryHandler : IRequestHandler<GetGamesListQuery, OperationResult<PagedResult<GameDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetGamesListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<GameDto>>> Handle(GetGamesListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Games.AsQueryable();
            var pagedEntities = await query.ToPagedResultAsync(request.Page, request.PageSize, cancellationToken);

            var pagedResultDto = new PagedResult<GameDto>
            {
                PageIndex = pagedEntities.PageIndex,
                PageSize = pagedEntities.PageSize,
                TotalCount = pagedEntities.TotalCount,
                TotalPages = pagedEntities.TotalPages,
                Results = pagedEntities.Results.Select(x => new GameDto
                {
                    Id = x.Id
                }).ToList()
            };

            return OperationResult<PagedResult<GameDto>>.Success(pagedResultDto);
        }
    }
}