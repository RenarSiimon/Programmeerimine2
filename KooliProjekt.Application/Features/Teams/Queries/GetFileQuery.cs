using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Teams.Queries
{
    public class GetFileQuery : IRequest<OperationResult<FileDescriptor>>
    {
        public int TeamId { get; set; }
        public string FileName { get; set; }
    }

    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, OperationResult<FileDescriptor>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileClient _fileClient;

        public GetFileQueryHandler(ApplicationDbContext context, IFileClient fileClient)
        {
            _context = context;
            _fileClient = fileClient;
        }

        public async Task<OperationResult<FileDescriptor>> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            if (!await _context.Teams.AnyAsync(team => team.Id == request.TeamId, cancellationToken))
            {
                return OperationResult<FileDescriptor>.NotFound("Team not found.");
            }

            var file = await _fileClient.GetAsync(request.TeamId, request.FileName, cancellationToken);
            return file == null
                ? OperationResult<FileDescriptor>.NotFound("File not found.")
                : OperationResult<FileDescriptor>.Success(file);
        }
    }
}
