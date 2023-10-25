using Azure.ResourceManager.Resources;
using Azure;
using AzureHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.ResourceManager.Storage;
using Azure.ResourceManager.Storage.Models;

namespace BasicServices.Tests
{
    internal class StorageTests
    {
        private ResourceGroupResource resourceGroup { get; set; }
        private string _rgName;

        [SetUp]
        public async Task SetUp()
        {
            _rgName = BaseClientExtension.CreateRandomName("StorageRG");
            BaseClient baseClient = new BaseClient();
            resourceGroup = await baseClient.CreateResourceGroup(_rgName);
        }

        [TearDown]
        public async Task TearDown()
        {
            resourceGroup.Delete(WaitUntil.Started);
        }

        [Test]
        public async Task BasicTest()
        {
            // Create storage account
            string storageAccountName = BaseClientExtension.CreateRandomName("aztestsa");
            StorageSku sku = new StorageSku(StorageSkuName.StandardGrs);
            StorageKind kind = StorageKind.Storage;
            StorageAccountCreateOrUpdateContent storageAccountInput = new StorageAccountCreateOrUpdateContent(sku, kind, resourceGroup.Data.Location);
            var saLro = await resourceGroup.GetStorageAccounts().CreateOrUpdateAsync(WaitUntil.Completed, storageAccountName, storageAccountInput);
            StorageAccountResource storageAccount = saLro.Value;
            await Console.Out.WriteLineAsync(storageAccount.Data.Name);

            // create blob
            BlobContainerCollection blobContainerCollection = storageAccount.GetBlobService().GetBlobContainers();
            string containerName = BaseClientExtension.CreateRandomName("container");
            BlobContainerData containerInput = new BlobContainerData();
            var containerLro = await blobContainerCollection.CreateOrUpdateAsync(WaitUntil.Completed, containerName, containerInput);
            BlobContainerResource container = containerLro.Value;
            await Console.Out.WriteLineAsync(container.Data.Name);
        }
    }
}
