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

namespace KooliProjekt.Application.Features.Tournaments.Queries
{
    public class GetTournamentsListQuery : IRequest<OperationResult<PagedResult<TournamentDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetTournamentsListQueryHandler : IRequestHandler<GetTournamentsListQuery, OperationResult<PagedResult<TournamentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetTournamentsListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<TournamentDto>>> Handle(GetTournamentsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Tournaments.AsQueryable();
            var pagedEntities = await query.ToPagedResultAsync(request.Page, request.PageSize, cancellationToken);

            var pagedResultDto = new PagedResult<TournamentDto>
            {
                PageIndex = pagedEntities.PageIndex,
                PageSize = pagedEntities.PageSize,
                TotalCount = pagedEntities.TotalCount,
                TotalPages = pagedEntities.TotalPages,
                Results = pagedEntities.Results.Select(x => new TournamentDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToList()
            };

            return OperationResult<PagedResult<TournamentDto>>.Success(pagedResultDto);
        }
    }
}