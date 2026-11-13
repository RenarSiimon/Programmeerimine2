using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public class LocalFileClient : IFileClient
    {
        private readonly FileStore _fileStore;

        public LocalFileClient(FileStore fileStore)
        {
            _fileStore = fileStore;
        }

        public async Task<FileDescriptor> SaveAsync(int teamId, string fileName, Stream content, CancellationToken cancellationToken)
        {
            var path = _fileStore.GetFilePath(teamId, fileName);
            await using (var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await content.CopyToAsync(output, cancellationToken);
            }

            return CreateDescriptor(path);
        }

        public Task<bool> DeleteAsync(int teamId, string fileName, CancellationToken cancellationToken)
        {
            var path = _fileStore.GetFilePath(teamId, fileName);
            var exists = File.Exists(path);
            if (exists)
            {
                File.Delete(path);
            }

            return Task.FromResult(exists);
        }

        public Task<FileDescriptor> GetAsync(int teamId, string fileName, CancellationToken cancellationToken)
        {
            var path = _fileStore.GetFilePath(teamId, fileName);
            return Task.FromResult(File.Exists(path) ? CreateDescriptor(path) : null);
        }

        public Task<IReadOnlyList<FileDescriptor>> ListAsync(int teamId, CancellationToken cancellationToken)
        {
            var directory = _fileStore.GetTeamDirectory(teamId);
            IReadOnlyList<FileDescriptor> files = Directory.EnumerateFiles(directory)
                .Select(CreateDescriptor)
                .ToList();
            return Task.FromResult(files);
        }

        private static FileDescriptor CreateDescriptor(string path)
        {
            var file = new FileInfo(path);
            return new FileDescriptor
            {
                FileName = file.Name,
                Path = file.FullName,
                Length = file.Length,
                LastModifiedUtc = file.LastWriteTimeUtc
            };
        }
    }
}
