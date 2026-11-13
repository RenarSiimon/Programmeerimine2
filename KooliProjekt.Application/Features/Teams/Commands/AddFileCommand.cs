using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Teams.Commands
{
    public class AddFileCommand : IRequest<OperationResult<FileDescriptor>>
    {
        public int TeamId { get; set; }
        public IFormFile File { get; set; }
    }

    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, OperationResult<FileDescriptor>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileClient _fileClient;

        public AddFileCommandHandler(ApplicationDbContext context, IFileClient fileClient)
        {
            _context = context;
            _fileClient = fileClient;
        }

        public async Task<OperationResult<FileDescriptor>> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            if (!await _context.Teams.AnyAsync(team => team.Id == request.TeamId, cancellationToken))
            {
                return OperationResult<FileDescriptor>.NotFound("Team not found.");
            }

            await using var stream = request.File.OpenReadStream();
            var descriptor = await _fileClient.SaveAsync(
                request.TeamId,
                Path.GetFileName(request.File.FileName),
                stream,
                cancellationToken);

            return OperationResult<FileDescriptor>.Success(descriptor);
        }
    }
}
