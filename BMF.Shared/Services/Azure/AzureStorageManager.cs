using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BMF.Shared.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BMF.Shared.Services.Azure
{
    public class AzureStorageManager : IAzureStorageManager
    {
        #region Fields

        private readonly AzureStorageConfig _azureStorageConfig;
        private BlobContainerClient blobContainer;

        #endregion

        #region Ctor

        public AzureStorageManager(IOptions<AzureStorageConfig> azureStorageConfig)
        {
            _azureStorageConfig = azureStorageConfig.Value;
        }

        #endregion

        #region Methods

        #endregion

        public async Task ConfigureContainer(string containerName)
        {
            try
            {
                blobContainer = new BlobContainerClient(_azureStorageConfig.ConnectionString, containerName);
                await blobContainer.CreateIfNotExistsAsync(PublicAccessType.Blob);
                if (!blobContainer.Exists())
                    throw new Exception("Blob container does not exist and could not be created.");
            }
            catch (Exception LogError)
            {
                throw new Exception("Failed to create/configure Blob Container.");
            }
        }

        public bool UploadFile(string containerName, string fileName, Stream fileStream, out string fileUrl, out string errorMessage, bool renameUsingGUID = true, Dictionary<string, string> metaData = null)
        {
            fileUrl = string.Empty;
            errorMessage = string.Empty;
            try
            {
                ConfigureContainer(containerName).Wait();

                if (blobContainer == null)
                    throw new Exception("Blob container not initalized.");

                string fileKeyName = fileName.SanitizeFileName().ToLower();
                if (renameUsingGUID)
                {
                    fileKeyName = $"{fileKeyName}-{Guid.NewGuid():N}{Path.GetExtension(fileName)}";
                }
                BlobClient blobUploadClient = blobContainer.GetBlobClient(fileKeyName);
                if (metaData != null && metaData.Count > 0)
                    blobUploadClient.SetMetadata(metaData);

                blobUploadClient.UploadAsync(fileStream, true).Wait();
                fileUrl = blobUploadClient.Uri.ToString();
                return true;
            }
            catch (Exception LogError)
            {
                errorMessage = LogError.Message;
            }
            return false;
        }

        public async Task<bool> DeleteFileAsync(string containerName, string fileName)
        {
            try
            {
                ConfigureContainer(containerName).Wait();
                if (string.IsNullOrWhiteSpace(fileName))
                    return false;

                BlobClient blobDeleteClient = blobContainer.GetBlobClient(fileName);
                if (blobDeleteClient != null)
                    if (await blobDeleteClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots))
                    {
                        return true;
                    }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
    }
}
