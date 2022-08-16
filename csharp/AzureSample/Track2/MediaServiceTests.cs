using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using AzureSample;
using NUnit.Framework;

namespace Track2
{
    internal class MediaServiceTests : TestBase
    {
        //private ResourceGroupResource _resourceGroup;
        //private MediaServiceResource _mediaService;

        //[OneTimeSetUp]
        //public async Task GlobalSetUp()
        //{
        //    // Create ArmClient
        //    ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        //    ArmClient armClient = new ArmClient(clientSecretCredential, subscription);

        //    // Create a resource group
        //    string rgName = "MediaService-Custom-RG-0000";
        //    ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
        //    ResourceGroupData rgData = new ResourceGroupData(AzureLocation.WestUS2) { };
        //    var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
        //    _resourceGroup = rgLro.Value;

        //    // Create a Storage account
        //    string storageAccountName = "azstorage22081053";
        //    StorageAccountCreateOrUpdateContent storagedata = new StorageAccountCreateOrUpdateContent(new StorageSku(StorageSkuName.StandardLrs), StorageKind.BlobStorage, _resourceGroup.Data.Location)
        //    {
        //        AccessTier = StorageAccountAccessTier.Hot,
        //    };
        //    var storage = await _resourceGroup.GetStorageAccounts().CreateOrUpdateAsync(WaitUntil.Completed, storageAccountName, storagedata);

        //    // Create a media service
        //    string mediaServiceName = "mediacustom0000";
        //    MediaServiceData data = new MediaServiceData(_resourceGroup.Data.Location);
        //    data.StorageAccounts.Add(new MediaServiceStorageAccount(MediaServiceStorageAccountType.Primary) { Id = storage.Value.Data.Id });
        //    var mediaServiceLro = await _resourceGroup.GetMediaServices().CreateOrUpdateAsync(WaitUntil.Completed, mediaServiceName, data);
        //    _mediaService = mediaServiceLro.Value;
        //}

        //[Test]
        //public async Task GetAll()
        //{
        //    await foreach (var item in _mediaService.GetLiveEvents().GetAllAsync())
        //    {
        //        Console.WriteLine(item.Data.Id);
        //    }
        //}
    }
}
