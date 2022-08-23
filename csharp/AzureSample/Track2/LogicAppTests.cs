using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage.Models;
using Azure.ResourceManager.Storage;
using NUnit.Framework;
using Azure.ResourceManager.Logic;
using System.IO;
using System.Text;
using Azure.ResourceManager.Logic.Models;
using Track2.Helper;
using Azure.ResourceManager.Network;

namespace Track2
{
    internal class LogicAppTests : Track2TestBase
    {
        private ResourceGroupResource _resourceGroup;
        private VirtualNetworkResource _vnet;
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
            ResourceGroupData rgData = new ResourceGroupData(_commonLocation) { };
            var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
            _resourceGroup = rgLro.Value;

            // Create virtual network
            string networkName = "vnet-0000";
            //_vnet = await CreateDefaultNetwork(_resourceGroup, networkName);
        }

        [Test]
        public async Task P0_IntegrationAccount_E2E()
        {
            var collection = _resourceGroup.GetIntegrationAccounts();

            // Create
            string integrationAccountName = "integrationAccount0000";
            IntegrationAccountData data = new IntegrationAccountData(_commonLocation)
            {
                SkuName = IntegrationAccountSkuName.Standard,
            };
            var integrationAccount = await collection.CreateOrUpdateAsync(WaitUntil.Completed, integrationAccountName, data);
            Console.WriteLine($"create: {integrationAccount.Value.Data.Name}");

            // Get
            var get = await collection.GetAsync(integrationAccountName);
            Console.WriteLine($"get: {get.Value.Data.Name}");

            // GetAll
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Id);
            }
        }

        [Test]
        public async Task P0_Workflow_E2E()
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

            LogicWorkflowData data = new LogicWorkflowData(_commonLocation)
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

        [Test]
        public async Task P0_IntegrationServiceEnvironment_E2E()
        {
            var collection = _resourceGroup.GetIntegrationServiceEnvironments();

           var v =await  _resourceGroup.GetVirtualNetworks().GetAsync("vnet-0000");

            //string serviceEnviromentName = "serviceEnviroment0000";
            //IntegrationServiceEnvironmentData data = new IntegrationServiceEnvironmentData(_commonLocation)
            //{
            //    Sku = new IntegrationServiceEnvironmentSku()
            //    {
            //        Capacity = 0,
            //        Name = IntegrationServiceEnvironmentSkuName.Developer
            //    },
            //    Properties = new IntegrationServiceEnvironmentProperties()
            //    {
            //        NetworkConfiguration = new IntegrationServiceNetworkConfiguration(),
            //    }
            //};
            //data.Properties.NetworkConfiguration.Subnets.Add(new LogicResourceReference() { Id = _vnet.Data.Subnets[0].Id });
            //data.Properties.NetworkConfiguration.Subnets.Add(new LogicResourceReference() { Id = _vnet.Data.Subnets[1].Id });
            //data.Properties.NetworkConfiguration.Subnets.Add(new LogicResourceReference() { Id = _vnet.Data.Subnets[2].Id });
            //data.Properties.NetworkConfiguration.Subnets.Add(new LogicResourceReference() { Id = _vnet.Data.Subnets[3].Id });
            //var serviceEnviroment = await collection.CreateOrUpdateAsync(WaitUntil.Completed, serviceEnviromentName, data);

            // GetAll
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Id);
            }
        }
    }
}
