namespace VeradelAddin.Application.Common.FileSystem.Contracts
{
    public interface IFileSystemServices
    {
        bool CheckOldVersion(string olderVersionFilename);
        void CopyAllFilesFromFolder(string tempFolderPath, string path);
        void CreateFolder(string path, string v);
        string CreateTempFolder();
        void MoveFile(string path, string olderVersionFilename, object draw, string extension);
        bool TryDeleteFile(string exportFileName);
    }
}