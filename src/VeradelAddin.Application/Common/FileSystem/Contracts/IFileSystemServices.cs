namespace VeradelAddin.Application.Common.FileSystem.Contracts
{
    public interface IFileSystemServices
    {
        bool CheckFileExists(string Filename);
        void CopyAllFilesFromDirectory(string BaseDirectoryPath, string DestinationDirectoryPath);
        void CreateDirectory(string BaseDirectoryPath, string DirectoryName);
        string GetNewTempDirectoryPath();
        bool MoveFile(string BaseDirectoryPath, string DestinationDirectoryName, string Filename, string Extension);
        bool TryDeleteFile(string Filename);
    }
}