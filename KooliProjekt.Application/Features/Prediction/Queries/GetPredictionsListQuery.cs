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

namespace KooliProjekt.Application.Features.Prediction.Queries
{
    public class GetPredictionsListQuery : IRequest<OperationResult<PagedResult<PredictionDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetPredictionsListQueryHandler : IRequestHandler<GetPredictionsListQuery, OperationResult<PagedResult<PredictionDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetPredictionsListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<PredictionDto>>> Handle(GetPredictionsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Predictions.AsQueryable();
            var pagedEntities = await query.ToPagedResultAsync(request.Page, request.PageSize, cancellationToken);

            var pagedResultDto = new PagedResult<PredictionDto>
            {
                PageIndex = pagedEntities.PageIndex,
                PageSize = pagedEntities.PageSize,
                TotalCount = pagedEntities.TotalCount,
                TotalPages = pagedEntities.TotalPages,
                Results = pagedEntities.Results.Select(x => new PredictionDto
                {
                    Id = x.Id
                }).ToList()
            };

            return OperationResult<PagedResult<PredictionDto>>.Success(pagedResultDto);
        }
    }
}