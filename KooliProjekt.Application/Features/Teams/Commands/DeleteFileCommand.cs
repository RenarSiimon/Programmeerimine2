using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Teams.Commands
{
    public class DeleteFileCommand : IRequest<OperationResult>
    {
        public int TeamId { get; set; }
        public string FileName { get; set; }
    }

    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileClient _fileClient;

        public DeleteFileCommandHandler(ApplicationDbContext context, IFileClient fileClient)
        {
            _context = context;
            _fileClient = fileClient;
        }

        public async Task<OperationResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            if (!await _context.Teams.AnyAsync(team => team.Id == request.TeamId, cancellationToken))
            {
                return OperationResult.NotFound("Team not found.");
            }

            var deleted = await _fileClient.DeleteAsync(request.TeamId, request.FileName, cancellationToken);
            return deleted
                ? OperationResult.Success()
                : OperationResult.NotFound("File not found.");
        }
    }
}
