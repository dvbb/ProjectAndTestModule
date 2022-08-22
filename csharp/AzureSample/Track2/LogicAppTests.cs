using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage.Models;
using AzureSample;
using Azure.ResourceManager.Storage;
using NUnit.Framework;
using Azure.ResourceManager.Logic;
using System.IO;
using System.Text;
using Azure.ResourceManager.Logic.Models;

namespace Track2
{
    internal class LogicAppTests : TestBase
    {
        private ResourceGroupResource _resourceGroup;
        private AzureLocation _commonLocation = AzureLocation.CentralUS;
        [OneTimeSetUp]
        public async Task GlobalSetUp()
        {
            // Create ArmClient
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            ArmClient armClient = new ArmClient(clientSecretCredential, subscription);

            // Create a resource group
            string rgName = "LogicAppRG-Custom-0000";
            ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
            ResourceGroupData rgData = new ResourceGroupData(AzureLocation.WestUS2) { };
            var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
            _resourceGroup = rgLro.Value;

        }

        [Test]
        public async Task IntegrationAccount_E2E()
        {
            var collection = _resourceGroup.GetIntegrationAccounts();

            string integrationAccountName = "integrationAccount0000";
            IntegrationAccountData data = new IntegrationAccountData(_commonLocation)
            {
                SkuName = IntegrationAccountSkuName.Standard,
            };
            var integrationAccount = await collection.CreateOrUpdateAsync(WaitUntil.Completed, integrationAccountName, data);

        }

        [Test]
        public async Task Workflow_E2E()
        {
            var collection = _resourceGroup.GetLogicWorkflows();

            string curDirectory = Directory.GetCurrentDirectory();
            string filepath = curDirectory + @"..\..\..\..\definition.json";

            StreamReader sr = new StreamReader(filepath, Encoding.Default);
            string definition = "";
            string content;
            while ((content = sr.ReadLine()) != null)
            {
                Console.WriteLine(content.ToString());
                definition += content.ToString();
            }

            LogicWorkflowData data = new LogicWorkflowData(AzureLocation.CentralUS)
            {
                Definition = new BinaryData(definition),
                IntegrationAccount = new LogicResourceReference()
                {
                    Id = new ResourceIdentifier("")
                },
            };
            var workflow = await collection.CreateOrUpdateAsync(WaitUntil.Completed, "workflowxfd", data);

            // GetAll
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Id);
            }

        }
    }
}
