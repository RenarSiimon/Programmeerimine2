using System;
using System.IO;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public class FileStore
    {
        private readonly string _rootPath;

        public FileStore(string rootPath)
        {
            _rootPath = Path.GetFullPath(rootPath ?? throw new ArgumentNullException(nameof(rootPath)));
            Directory.CreateDirectory(_rootPath);
        }

        public string GetTeamDirectory(int teamId)
        {
            if (teamId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(teamId));
            }

            var directory = Path.Combine(_rootPath, teamId.ToString());
            Directory.CreateDirectory(directory);
            return directory;
        }

        public string GetFilePath(int teamId, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || fileName != Path.GetFileName(fileName))
            {
                throw new ArgumentException("A valid file name is required.", nameof(fileName));
            }

            return Path.Combine(GetTeamDirectory(teamId), fileName);
        }
    }
}
