// https://www.nuget.org/packages/Azure.Storage.Blobs/
using System;
using System.Globalization;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace sasser;
class SAS : BackgroundService
{

    private readonly ILogger<Worker> _logger;

    public SAS(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    // this is bollocks c#, do it better
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var task = Task.Run(() => SAS.CreateToken());
            await task;
        }
    }

    readonly static short timeout_in_hours = 24;

    static string accountName = "";
    static string accountKey = "";
    static string containerName = "";

    static SAS()
    {
        //  from the config 
        // SAS.accountName = "your-storage-account-name";
        // SAS.accountKey = "your-storage-account-key";
        // SAS.containerName = "your-container-name";
    }
    public static string CreateToken()
    {
        StorageCredentials credentials = new StorageCredentials(accountName, accountKey);
        CloudStorageAccount storageAccount = new CloudStorageAccount(credentials, true);
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        CloudBlobContainer container = blobClient.GetContainerReference(containerName);

        SharedAccessBlobPolicy sasPolicy = new SharedAccessBlobPolicy()
        {
            Permissions = SharedAccessBlobPermissions.Read,
            SharedAccessExpiryTime = DateTime.UtcNow.AddHours(timeout_in_hours)
        };

        string sasToken = container.GetSharedAccessSignature(sasPolicy);

        Console.WriteLine(sasToken); // for container this is logging
        return sasToken;
    }
}
