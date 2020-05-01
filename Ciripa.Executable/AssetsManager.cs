using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Ciripa.Executable
{
    internal class AssetsManager : IDisposable
    {
        private const string ResourcesFolder = "Resources";
        private readonly string _archiveTempFilePath;
        private ZipArchive _archive;

        public AssetsManager()
        {
            _archiveTempFilePath = Path.GetTempFileName();
        }

        public ZipArchive Archive
        {
            get
            {
                if (_archive != null)
                {
                    return _archive;
                }

                using (var archiveStream = GetResource("Assets.zip"))
                using (var archiveFileStream = new FileStream(_archiveTempFilePath, FileMode.OpenOrCreate))
                {
                    archiveStream.CopyTo(archiveFileStream);
                }

                _archive = ZipFile.OpenRead(_archiveTempFilePath);
                return _archive;
            }
        }

        public static Stream GetResource(string resourceName)
        {
            var assembly = typeof(AssetsManager).Assembly;
            var pathBuilder = new StringBuilder(typeof(AssetsManager).Namespace);

            pathBuilder.Append($".{ResourcesFolder}");
            pathBuilder.Append($".{resourceName}");

            return assembly.GetManifestResourceStream(pathBuilder.ToString());
        }

        public void Dispose()
        {
            if (_archive == null)
            {
                return;
            }

            _archive.Dispose();
            _archive = null;
            File.Delete(_archiveTempFilePath);
        }
    }
}