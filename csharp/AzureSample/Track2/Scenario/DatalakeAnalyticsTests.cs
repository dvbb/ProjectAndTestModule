using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager.DataLakeAnalytics;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Track2.Helper;
using Azure.ResourceManager.DataLakeAnalytics.Models;

namespace Track2
{
    internal class DatalakeAnalyticsTests : Track2TestBase
    {
        private ResourceGroupResource _resourceGroup;
        private AzureLocation _commonLocation = AzureLocation.CentralUS;

        [OneTimeSetUp]
        public async Task GlobalSetUp()
        {
            // Create ArmClient
            var options = new ArmClientOptions();
            options.SetApiVersion("microsoft.DataLakeAnalytics/accounts", "2018-02-01-preview");
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            ArmClient armClient = new ArmClient(clientSecretCredential, subscription, options);

            // Create a resource group
            string rgName = "DataLakeAnalyticsCustomRG0000";
            ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
            ResourceGroupData rgData = new ResourceGroupData(_commonLocation) { };
            var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
            _resourceGroup = rgLro.Value;
        }

        [Test]
        public async Task DataAnalytics()
        {
            //DataLakeAnalyticsAccountCollection
            DataLakeAnalyticsAccountCollection colleciton = _resourceGroup.GetDataLakeAnalyticsAccounts();

            // create
            string accountName = "azdatalakesdccount0000";
            var paras = new List<DataLakeStoreForDataLakeAnalyticsAccountCreateOrUpdateContent>()
            {
                new DataLakeStoreForDataLakeAnalyticsAccountCreateOrUpdateContent("test")
                {
                }
            };
            var data = new DataLakeAnalyticsAccountCreateOrUpdateContent(_commonLocation, "testadls", paras)
            {
            };
            await colleciton.CreateOrUpdateAsync(Azure.WaitUntil.Completed, accountName, data);

            // getall
            var basices = await colleciton.GetAllAsync().ToEnumerableAsync();
            Console.WriteLine(basices.Count);
            foreach (var item in basices)
            {
                Console.WriteLine(item);
            }
        }
    }
}
