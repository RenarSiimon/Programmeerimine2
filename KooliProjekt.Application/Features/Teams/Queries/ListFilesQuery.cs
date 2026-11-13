using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Teams.Queries
{
    public class ListFilesQuery : IRequest<OperationResult<IReadOnlyList<FileDescriptor>>>
    {
        public int TeamId { get; set; }
    }

    public class ListFilesQueryHandler : IRequestHandler<ListFilesQuery, OperationResult<IReadOnlyList<FileDescriptor>>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileClient _fileClient;

        public ListFilesQueryHandler(ApplicationDbContext context, IFileClient fileClient)
        {
            _context = context;
            _fileClient = fileClient;
        }

        public async Task<OperationResult<IReadOnlyList<FileDescriptor>>> Handle(ListFilesQuery request, CancellationToken cancellationToken)
        {
            if (!await _context.Teams.AnyAsync(team => team.Id == request.TeamId, cancellationToken))
            {
                return OperationResult<IReadOnlyList<FileDescriptor>>.NotFound("Team not found.");
            }

            var files = await _fileClient.ListAsync(request.TeamId, cancellationToken);
            return OperationResult<IReadOnlyList<FileDescriptor>>.Success(files);
        }
    }
}
