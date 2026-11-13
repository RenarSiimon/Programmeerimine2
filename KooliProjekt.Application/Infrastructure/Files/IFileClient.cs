using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public interface IFileClient
    {
        Task<FileDescriptor> SaveAsync(int teamId, string fileName, Stream content, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int teamId, string fileName, CancellationToken cancellationToken);
        Task<FileDescriptor> GetAsync(int teamId, string fileName, CancellationToken cancellationToken);
        Task<IReadOnlyList<FileDescriptor>> ListAsync(int teamId, CancellationToken cancellationToken);
    }
}
