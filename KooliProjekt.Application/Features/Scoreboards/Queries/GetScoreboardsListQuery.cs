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

namespace KooliProjekt.Application.Features.Scoreboards.Queries
{
    public class GetScoreboardsListQuery : IRequest<OperationResult<PagedResult<ScoreboardDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetScoreboardsListQueryHandler : IRequestHandler<GetScoreboardsListQuery, OperationResult<PagedResult<ScoreboardDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetScoreboardsListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<ScoreboardDto>>> Handle(GetScoreboardsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Scoreboards.AsQueryable();
            var pagedEntities = await query.ToPagedResultAsync(request.Page, request.PageSize, cancellationToken);

            var pagedResultDto = new PagedResult<ScoreboardDto>
            {
                PageIndex = pagedEntities.PageIndex,
                PageSize = pagedEntities.PageSize,
                TotalCount = pagedEntities.TotalCount,
                TotalPages = pagedEntities.TotalPages,
                Results = pagedEntities.Results.Select(x => new ScoreboardDto
                {
                    Id = x.Id,
                    Place = x.Place,
                    Points = x.Points
                }).ToList()
            };

            return OperationResult<PagedResult<ScoreboardDto>>.Success(pagedResultDto);
        }
    }
}