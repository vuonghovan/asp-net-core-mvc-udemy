using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cloud.Service
{
    public interface ICoudStorageService
    {
        Task<bool> UploadFileToBlobAsync(IFormFile formFile, string keyFile);
        Task<byte[]> DownloadFileToBlobAsync(string keyFile);
        Task<bool> DeleteFileToBlobAsync(string keyFile);
    }
}
