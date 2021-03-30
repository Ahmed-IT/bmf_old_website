using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BMF_WebSite.Models
{
    public class StorageManager
    {
        public static void upload_ToBlob(string fileToUpload, string azure_ContainerName)
        {
            string file_extension,
            filename_withExtension,
            storageAccount_connectionString;
            Stream file;

            //Copy the storage account connection string from Azure portal     
            storageAccount_connectionString = "DefaultEndpointsProtocol=https;AccountName=bmfstorage;AccountKey=[REDACTED];EndpointSuffix=core.windows.net";

            // << reading the file as filestream from local machine >>    
            file = new FileStream(fileToUpload, FileMode.Open);

            CloudStorageAccount mycloudStorageAccount = CloudStorageAccount.Parse(storageAccount_connectionString);
            CloudBlobClient blobClient = mycloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(azure_ContainerName);

            //checking the container exists or not  
            if (container.CreateIfNotExists())
            {

                container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess =
                  BlobContainerPublicAccessType.Blob
                });

            }

            //reading file name & file extention    
            file_extension = Path.GetExtension(fileToUpload);
            filename_withExtension = Path.GetFileName(fileToUpload);

            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(filename_withExtension);
            cloudBlockBlob.Properties.ContentType = file_extension;

            cloudBlockBlob.UploadFromStreamAsync(file); // << Uploading the file to the blob >>  

        }

    }
}