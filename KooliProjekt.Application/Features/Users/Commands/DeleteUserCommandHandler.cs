using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Users.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
            if (user == null)
            {
                return OperationResult.NotFound("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}