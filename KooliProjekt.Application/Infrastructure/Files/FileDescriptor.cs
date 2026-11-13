using System;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public class FileDescriptor
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public long Length { get; set; }
        public DateTime LastModifiedUtc { get; set; }
    }
}
