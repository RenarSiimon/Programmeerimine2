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

namespace KooliProjekt.Application.Features.Users.Queries
{
    public class GetUsersListQuery : IRequest<OperationResult<PagedResult<UserDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, OperationResult<PagedResult<UserDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetUsersListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<UserDto>>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users.AsQueryable();
            var pagedEntities = await query.ToPagedResultAsync(request.Page, request.PageSize, cancellationToken);

            var pagedResultDto = new PagedResult<UserDto>
            {
                PageIndex = pagedEntities.PageIndex,
                PageSize = pagedEntities.PageSize,
                TotalCount = pagedEntities.TotalCount,
                TotalPages = pagedEntities.TotalPages,
                Results = pagedEntities.Results.Select(x => new UserDto
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email
                }).ToList()
            };

            return OperationResult<PagedResult<UserDto>>.Success(pagedResultDto);
        }
    }
}