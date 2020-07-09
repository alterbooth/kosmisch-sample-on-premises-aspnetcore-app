using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace Kosmisch.Sample.OnPremisesAspnetApp.Helpers
{
    public static class FileHelper
    {
        public static void WriteJson(string json)
        {
            var connectionString = Environment.GetEnvironmentVariable("StorageConnectionString") ?? "UseDevelopmentStorage=true";
            CloudStorageAccount.TryParse(connectionString, out var storageAccount);

            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            var containerName = Environment.GetEnvironmentVariable("BlobContainerName") ?? "mycontainer";
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            cloudBlobContainer.CreateAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var permissions = cloudBlobContainer.GetPermissionsAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
            cloudBlobContainer.SetPermissionsAsync(permissions).ConfigureAwait(false).GetAwaiter().GetResult();

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference($"sample-data-{DateTime.Now.ToString("yyyyMMddHHmmss")}.json");
            cloudBlockBlob.UploadTextAsync(json).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}