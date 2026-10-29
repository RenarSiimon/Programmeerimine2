using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.Application.Infrastructure.Paging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;

namespace KooliProjekt.Application.Features.Users.Queries
{
    public class GetUserDetailsQuery : IRequest<OperationResult<UserDto>>
    {
        public int Id { get; set; }
    }

    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, OperationResult<UserDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetUserDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<UserDto>> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users
                .Where(u => u.Id == request.Id)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return OperationResult<UserDto>.NotFound("User not found.");
            }

            return OperationResult<UserDto>.Success(entity);
        }
    }
}