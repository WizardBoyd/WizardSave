using System.IO;

namespace WizardSave
{
    public interface IFilePathProvider
    {
        string FilePath { get; }
    }
    
    public interface IFileKeyValueStore : IKeyValueStore, IFilePathProvider {}

    public static class IFilePathProviderProviderExtensions
    {
        public static FileStream OpenFileReadStream(this IFilePathProvider filenameProvider)
        {
            string filePath = filenameProvider.FilePath;
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return null;
            }
            else
            {
                return File.OpenRead(filePath);
            }
        }

        public static FileStream OpenFileWriteStream(this IFilePathProvider filenameProvider, FileMode fileMode = FileMode.Create)
        {
            string filePath = filenameProvider.FilePath;
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return File.Open(filePath, fileMode, FileAccess.Write);
        }
    }
}