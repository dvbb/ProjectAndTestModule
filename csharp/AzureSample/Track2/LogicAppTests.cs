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
using Azure.ResourceManager.KeyVault;

namespace Track2
{
    internal class LogicAppTests : Track2TestBase
    {
        private ResourceGroupResource _resourceGroup;
        private VirtualNetworkResource _vnet;
        private IntegrationAccountResource _integrationAccount;
        private AzureLocation _commonLocation = AzureLocation.CentralUS;

        [OneTimeSetUp]
        public async Task GlobalSetUp()
        {
            // Create ArmClient
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            ArmClient armClient = new ArmClient(clientSecretCredential, subscription);

            // Create a resource group
            string rgName = "LogicAppRG-Custom-1000";
            ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
            ResourceGroupData rgData = new ResourceGroupData(_commonLocation) { };
            var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
            _resourceGroup = rgLro.Value;

            // Create Integration Account
            _integrationAccount = await CreateIntegrationAccount(_resourceGroup, "integration0000");
        }

        private async Task<IntegrationAccountResource> CreateIntegrationAccount(ResourceGroupResource resourceGroup, string integrationAccountName)
        {
            IntegrationAccountData data = new IntegrationAccountData(resourceGroup.Data.Location)
            {
                SkuName = IntegrationAccountSkuName.Standard,
            };
            var integrationAccount = await resourceGroup.GetIntegrationAccounts().CreateOrUpdateAsync(WaitUntil.Completed, integrationAccountName, data);
            return integrationAccount.Value;
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

            // Create - takes 30 seconds
            string workflowName = "workflow0000";
            string filepath = Directory.GetCurrentDirectory() + @"..\..\..\..\definitionV2.json";
            byte[] definition = File.ReadAllBytes(filepath);
            LogicWorkflowData data = new LogicWorkflowData(_resourceGroup.Data.Location)
            {
                Definition = new BinaryData(definition),
                IntegrationAccount = new LogicResourceReference() { Id = _integrationAccount.Data.Id },
            };
            var workflow = await collection.CreateOrUpdateAsync(WaitUntil.Completed, workflowName, data);

            // GetAll
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Id);
            }

        }

        [Test]
        public async Task P0_IntegrationServiceEnvironment_E2E()
        {
            _vnet = await CreateDefaultNetwork(_resourceGroup, "vnet1000");
            var collection = _resourceGroup.GetIntegrationServiceEnvironments();

            // It will cost 6 hours..
            string serviceEnviromentName = "serviceEnviroment0000";
            IntegrationServiceEnvironmentData data = new IntegrationServiceEnvironmentData(_commonLocation)
            {
                Sku = new IntegrationServiceEnvironmentSku()
                {
                    Capacity = 0,
                    Name = IntegrationServiceEnvironmentSkuName.Developer
                },
                Properties = new IntegrationServiceEnvironmentProperties()
                {
                    NetworkConfiguration = new IntegrationServiceNetworkConfiguration(),
                }
            };
            data.Properties.NetworkConfiguration.Subnets.Add(new LogicResourceReference() { Id = _vnet.Data.Subnets[0].Id });
            data.Properties.NetworkConfiguration.Subnets.Add(new LogicResourceReference() { Id = _vnet.Data.Subnets[1].Id });
            data.Properties.NetworkConfiguration.Subnets.Add(new LogicResourceReference() { Id = _vnet.Data.Subnets[2].Id });
            data.Properties.NetworkConfiguration.Subnets.Add(new LogicResourceReference() { Id = _vnet.Data.Subnets[3].Id });
            //var serviceEnviroment = await collection.CreateOrUpdateAsync(WaitUntil.Completed, serviceEnviromentName, data);

            // Exist
            bool flag = await collection.ExistsAsync(serviceEnviromentName);
            Console.WriteLine(flag);

            // Get 
            var get = await collection.GetAsync(serviceEnviromentName);
            Console.WriteLine($"get: {get.Value.Data.Name}");

            // GetAll
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Id);
            }

            // Delete
            await get.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync(serviceEnviromentName);
            Console.WriteLine(flag);
        }

        [Test]
        public async Task P1_IntegrationAccount_Maps()
        {
            var collection = _integrationAccount.GetIntegrationAccountMaps();
            string mapName = "map0000";
            IntegrationAccountMapData data = new IntegrationAccountMapData(_integrationAccount.Data.Location, IntegrationAccountMapType.Xslt30)
            {
                Content = Xslt30MapContent,
                ContentType = "application/xml"
            };
            var map = await collection.CreateOrUpdateAsync(WaitUntil.Completed, mapName, data);
        }

        private string Xslt30MapContent
        {
            get
            {
                return @"<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' xmlns:xs='http://www.w3.org/2001/XMLSchema' version='3.0'>
	                        <xsl:output method='text'/>
	                        <xsl:template match='/'>
		                        <xsl:value-of select='company/employee/name'/>
		                        <xsl:variable name='test'>
			                        <xsl:text>company/employee/name</xsl:text>
		                        </xsl:variable>
		                        <xsl:evaluate xpath='$test'/>
	                        </xsl:template>
                        </xsl:stylesheet>";
            }
        }
    }
}
