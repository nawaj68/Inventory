using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interface
{
    public interface IServerDirectoryService
    {
        bool Exists(string path);
        bool FileExists(string path);
        string GetApplicationPath();
        string GetWWWRootPath();



        void RemoveFile(string path);
        void SaveFile(string path, byte[] buffer);
        void SaveFile(string path, IFormFile formFile);
        Task SaveFileAsync(string path, IFormFile formFile);

        void MoveFileUsingRelativePath(string fromDirectoryPath, string toDirectoryPath);
        void MoveFileFromPhysicalPathToServer(string fromDirectoryPath, string toDirectoryPath);
        void CopyFileUsingRelativePath(string fromDirectoryPath, string toDirectoryPath);
        List<string> GetAllFileName(string directory);
        List<string> GetAllFileName(string directory, string url);
        List<string> GetAllFiles(string directory);
    }
}
