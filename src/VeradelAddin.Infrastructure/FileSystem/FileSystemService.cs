using System;
using System.IO;
using VeradelAddin.Application.Common.FileSystem.Contracts;

namespace VeradelAddin.Infrastructure.FileSystem
{
    public class FileSystemService : IFileSystemServices
    {
        /// <summary>
        /// Checks whether a file exists
        /// </summary>
        /// <param name="Filename">Path + filename + extension</param>
        /// <returns>True if file exists</returns>
        public bool CheckFileExists(string Filename)
        {
            return File.Exists(Filename);
        }

        /// <summary>
        /// Copies ALL the files and directories from a base directory, to a given Directory
        /// </summary>
        /// <param name="BaseDirectoryPath">Path to the Directory from which the files are being moved</param>
        /// <param name="DestinationDirectoryPath">Destination Directory</param>
        public void CopyAllFilesFromDirectory(string BaseDirectoryPath, string DestinationDirectoryPath)
        {
            string[] files = Directory.GetFiles(BaseDirectoryPath, "*", SearchOption.AllDirectories);

            foreach (string oldFileName in files)
            {
                string newFilename = System.IO.Path.Combine(DestinationDirectoryPath, System.IO.Path.GetFileName(oldFileName));
                File.Copy(oldFileName, newFilename);
            }
        }


        /// <summary>
        /// Creates a new Directory based on the path and a new Directory name, e.g. C:\abc, bcd ->  C:\abc\bcd 
        /// </summary>
        /// <param name="BaseDirectoryPath">Path to the base directy</param>
        /// <param name="DirectoryName">New directory</param>
        public void CreateDirectory(string BaseDirectoryPath, string DirectoryName)
        {
            string DirectoryToCreate = Path.Combine(BaseDirectoryPath, DirectoryName);
            Directory.CreateDirectory(DirectoryToCreate);
        }

        /// <summary>
        /// Gets a randon name directory inside the temp system directory
        /// </summary>
        /// <returns>Path to a temp directory</returns>
        public string GetNewTempDirectoryPath()
        {
            string tempPath = Path.GetTempPath();
            string Directoryname = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            return Path.Combine(tempPath, Directoryname);
        }


        /// <summary>
        /// Moves a File to a directory that is located inside the directory file.
        /// </summary>
        /// <param name="BaseDirectoryPath">Path to the filename</param>
        /// <param name="DestinationDirectoryName">Destination folder (must be located in the Base Directory)</param>
        /// <param name="Filename">Filename without extension</param>
        /// <param name="Extension">extension</param>
        /// <returns>True if the file could be moved, false when exception occured</returns>

        public bool MoveFile(string BaseDirectoryPath, string DestinationDirectoryName, string Filename, string Extension)
        {
            string filenameWithExtension = string.Concat(Filename, Extension);

            string baseFileName = Path.Combine(BaseDirectoryPath, filenameWithExtension);
            string destinationFilname = Path.Combine(BaseDirectoryPath, DestinationDirectoryName, filenameWithExtension);

            try
            {
                File.Move(baseFileName, destinationFilname);
                return true;
            }
            catch (Exception ex) {
                return false;
            }

        }

        /// <summary>
        /// Tries to delete a file
        /// </summary>
        /// <param name="Filename">Filaname</param>
        /// <returns>True if succeded</returns>
        public bool TryDeleteFile(string Filename)
        {
            try
            {
                File.Delete(Filename);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }



    }
}
