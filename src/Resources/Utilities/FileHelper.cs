using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Utilities
{
    public static class FileHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns>byte array</returns>
        public static async Task<byte[]> GetByteArrayFromImageAsync(this IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                await file.CopyToAsync(target);
                return target.ToArray();
            }
        }

        public static Task<bool> UploadFile(string path, IFormFile file, string fileName = "")
        {
            if (file == null || file.Length == 0)
            {
                return Task.FromResult(true);
            }

            if (string.IsNullOrEmpty(path))
                throw new System.Exception("Path file cannot null");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                path = Path.Combine(path, fileName);
            }
            else
            {
                path = Path.Combine(path, file.FileName);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyToAsync(stream).Wait();
            }

            return Task.FromResult(true);
        }

        public static void DeleteFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new System.Exception("Path file cannot null");

            File.Delete(path);
        }
    }
}
