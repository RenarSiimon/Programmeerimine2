using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Users.Commands
{
    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, OperationResult<int>>
    {
        private readonly ApplicationDbContext _context;

        public SaveUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<int>> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            User user;

            if (request.Id == 0)
            {
                user = new User();
                _context.Users.Add(user);
            }
            else
            {
                user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
                if (user == null)
                {
                    return OperationResult<int>.NotFound("User not found");
                }
            }

            user.Username = request.Username;

            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult<int>.Success(user.Id);
        }
    }
}