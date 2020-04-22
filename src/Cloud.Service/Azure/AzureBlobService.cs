using Infrastructure.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using Utilities;

namespace Cloud.Service.Azure
{
    public class AzureBlobService : ICoudStorageService
    {
        AzureConfig _azureConfig;

        public AzureBlobService(IOptions<AzureConfig> azureConfig)
        {
            _azureConfig = azureConfig.Value;
        }

        public async Task<bool> UploadFileToBlobAsync(IFormFile formFile, string keyFile)
        {
            try
            {
                if (!_azureConfig.IS_ENABLE)
                    return await Task.FromResult(true);

                if (formFile == null)
                    throw new Exception("File is null!");
                if (string.IsNullOrEmpty(keyFile))
                    throw new Exception("Key of file cannot empty");

                //  Retrieve storage account from connection string.
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_azureConfig.CONNECTION_STRING);
                // Create the blob client.
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                // Retrive reference to a previously created container.
                CloudBlobContainer container = cloudBlobClient.GetContainerReference(_azureConfig.CONTAINER_NAME);

                if (await container.ExistsAsync())
                {
                    var byteArray = await FileHelper.GetByteArrayFromImageAsync(formFile);

                    CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(keyFile);
                    cloudBlockBlob.Properties.ContentType = formFile.ContentType;
                    await cloudBlockBlob.UploadFromByteArrayAsync(byteArray, 0, (int)formFile.Length);

                    return await Task.FromResult(true);
                }
                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<bool> DeleteFileToBlobAsync(string keyFile)
        {
            try
            {
                if (!_azureConfig.IS_ENABLE)
                    return await Task.FromResult(true);

                if (string.IsNullOrEmpty(keyFile))
                    throw new Exception("Key of file cannot empty");

                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureConfig.CONNECTION_STRING);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference(_azureConfig.CONTAINER_NAME);

                if (await container.ExistsAsync())
                {
                    // Get the directory from which we need to delete the files
                    CloudBlobDirectory dira = container.GetDirectoryReference(_azureConfig.CONNECTION_STRING);

                    // Retrieve reference to a blob named "myblob.csv".
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(keyFile);
                    if (await blockBlob.ExistsAsync())
                    {
                        // Delete the blob.
                        var temp = blockBlob.DeleteIfExistsAsync().Result;
                        return await Task.FromResult(true);
                    }
                }

                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> DownloadFileToBlobAsync(string keyFile)
        {
            try
            {
                if (!_azureConfig.IS_ENABLE)
                    return null;

                if (string.IsNullOrEmpty(keyFile))
                    throw new Exception("Key of file cannot empty");

                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureConfig.CONNECTION_STRING);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference(_azureConfig.CONTAINER_NAME);

                if (await container.ExistsAsync())
                {
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(keyFile);

                    if (await blockBlob.ExistsAsync())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await blockBlob.DownloadToStreamAsync(ms);
                            return ms.ToArray();
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
