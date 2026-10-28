using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Identity;
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
        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient blobContainer;

        #endregion

        #region Ctor

        public AzureStorageManager(IOptions<AzureStorageConfig> azureStorageConfig)
        {
            _azureStorageConfig = azureStorageConfig.Value;
            var isProduction = string.Equals(
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                "Production",
                StringComparison.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(_azureStorageConfig.ConnectionString))
            {
                _blobServiceClient = new BlobServiceClient(_azureStorageConfig.ConnectionString);
            }
            else if (isProduction)
            {
                if (string.IsNullOrWhiteSpace(_azureStorageConfig.AccountName))
                    throw new InvalidOperationException("Azure storage is not configured. Set AzureStorageConfig__AccountName.");

                var serviceUri = new Uri($"https://{_azureStorageConfig.AccountName}.blob.core.windows.net/");
                _blobServiceClient = new BlobServiceClient(serviceUri, new ManagedIdentityCredential());
            }
            else if (!string.IsNullOrWhiteSpace(_azureStorageConfig.AccountName))
            {
                var serviceUri = new Uri($"https://{_azureStorageConfig.AccountName}.blob.core.windows.net/");
                _blobServiceClient = new BlobServiceClient(serviceUri, new DefaultAzureCredential());
            }
            else
            {
                throw new InvalidOperationException("Azure storage is not configured. Set AzureStorageConfig__AccountName or a local connection string.");
            }
        }

        #endregion

        #region Methods

        #endregion

        public async Task ConfigureContainer(string containerName)
        {
            try
            {
                blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
                await blobContainer.CreateIfNotExistsAsync(PublicAccessType.Blob);
                if (!blobContainer.Exists())
                    throw new Exception("Blob container does not exist and could not be created.");
            }
            catch (Exception error)
            {
                throw new InvalidOperationException("Failed to create/configure Blob Container.", error);
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
