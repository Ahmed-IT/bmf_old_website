using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BMF_WebSite.Models
{
    public class StorageManager
    {
        CloudBlobClient blobClient;
        CloudBlobContainer blobContainer;
        CloudStorageAccount storageAccount;

        /// <summary>
        /// constructor
        /// </summary>
        public StorageManager()
        {
            // Retrieve storage account information from connection string
            // How to create a storage connection string - http://msdn.microsoft.com/en-us/library/azure/ee758697.aspx
            //storageAccount = CloudStorageAccount.Parse(Microsoft.Azure.CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Parse the connection string and return a reference to the storage account.
            var storageConnectionString = Environment.GetEnvironmentVariable("AzStorageAccount");
            if (string.IsNullOrWhiteSpace(storageConnectionString))
                storageConnectionString = CloudConfigurationManager.GetSetting("AzStorageAccount");
            if (string.IsNullOrWhiteSpace(storageConnectionString))
                throw new InvalidOperationException("Azure storage is not configured. Set the AzStorageAccount app setting or environment variable.");

            storageAccount = CloudStorageAccount.Parse(storageConnectionString);
        }

        /// <summary>
        /// Configure and create a Blob container read permission will be set to public
        /// </summary>
        /// <param name="blobContainerName"></param>
        /// <returns></returns>
        async public Task CreateAndConfigureAsync(string blobContainerName)
        {
            try
            {
                // Create a blob client for interacting with the blob service.
                blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                blobContainer = blobClient.GetContainerReference(blobContainerName.ToLower());

                // Create the container if it doesn't already exist.
                //await blobContainer.CreateIfNotExistsAsync();

                // To view the uploaded blob in a browser, you have two options. The first option is to use a Shared Access Signature (SAS) token to delegate  
                // access to the resource. See the documentation links at the top for more information on SAS. The second approach is to set permissions  
                // to allow public access to blobs in this container. Comment the line below to not use this approach and to use SAS. Then you can view the image  
                // using: https://[InsertYourStorageAccountNameHere].blob.core.windows.net/webappstoragedotnet-imagecontainer/FileName 
                //await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                //blobContainer.CreateIfNotExists();
                //blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });

                // Create the "images" container if it doesn't already exist.
                if (await blobContainer.CreateIfNotExistsAsync())
                {
                    // Enable public access on the newly created "images" container
                    await blobContainer.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess =
                                BlobContainerPublicAccessType.Blob
                        });
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// UploadPhoto to Blob Storage
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="photoToUpload"></param>
        /// <returns></returns>
        async public Task<string> UploadPhotoAsync(string blobContainerName, string filename, HttpPostedFileBase photoToUpload)
        {
            //  TODO: make this configuraable in configuration
            await CreateAndConfigureAsync(blobContainerName);

            if (photoToUpload == null || photoToUpload.ContentLength == 0)
            {
                return null;
            }

            string fullPath = null;
            //Stopwatch timespan = Stopwatch.StartNew();

            try
            {
                //CloudStorageAccount storageAccount = StorageUtils.StorageAccount;

                //// Create the blob client and reference the container
                //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                //CloudBlobContainer container = blobClient.GetContainerReference("images");

                // Create a unique name for the images we are about to upload
                //string imageName = String.Format("sns-{0}{1}",
                //    Guid.NewGuid().ToString(),
                //    Path.GetExtension(photoToUpload.FileName));

                string imageName = $"{filename}-{Guid.NewGuid().ToString("N")}{Path.GetExtension(photoToUpload.FileName)}";


                // Upload image to Blob Storage
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(imageName.ToLower());
                blockBlob.Properties.ContentType = photoToUpload.ContentType;
                await blockBlob.UploadFromStreamAsync(photoToUpload.InputStream);

                fullPath = blockBlob.Uri.ToString();

                //timespan.Stop();
                //log.TraceApi("Blob Service", "PhotoService.UploadPhoto", timespan.Elapsed, "imagepath={0}", fullPath);
            }
            catch (Exception ex)
            {
                //log.Error(ex, "Error upload photo blob to storage")
                //throw;
                return null;
            }

            return fullPath;
        }
    }
}