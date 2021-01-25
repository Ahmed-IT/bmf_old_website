using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BMF.Shared.Services.Azure
{
    public interface IAzureStorageManager
    {
        public Task ConfigureContainer(string containerName);

        public bool UploadFile(string containerName, string fileName, Stream fileStream, out string fileUrl, out string errorMessage, bool renameUsingGUID = true, Dictionary<string, string> metaData = null);

        public Task<bool> DeleteFileAsync(string containerName, string fileName);
    }
}
