using Azure;
using Azure.Core;
using Azure.ResourceManager.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azure.ResourceManager.SecurityCenter;
using Track2.Helper;
using Azure.ResourceManager.SecurityCenter.Models;
using System.Linq;
using System.Threading;
using Azure.ResourceManager.Logic;
using Azure.ResourceManager.Logic.Models;
using System.IO;
using System.Reflection;

namespace Track2
{
    internal class SecurityCenterTests : Track2TestBase
    {
        private ResourceGroupResource _resourceGroup;

        [SetUp]
        public async Task TestSetUp()
        {
            _resourceGroup = await CreateResourceGroup("IotSecurityRG0000", AzureLocation.EastUS);
        }

        [Test]
        public async Task AdaptiveNetworkHardeningE2E()
        {
            var Collection = _resourceGroup.GetAdaptiveNetworkHardenings("Microsoft.Compute", "virtualMachines", "vm0000");
            var list = await Collection.GetAllAsync().ToEnumerableAsync();
            Console.WriteLine(list.Count);
        }

        [Test]
        public async Task AllowedConnectionsTest()
        {
            var allowedConnectionsResourceCollection = _resourceGroup.GetAllowedConnectionsResources();

            // prerequisites
            var vnet = await CreateDefaultNetwork(_resourceGroup, "vnet0000");
            var networkInterface = await CreateDefaultNetworkInterface(_resourceGroup, vnet, "networkInterface0000");
            var vm = await CreateDefaultVirtualMachine(_resourceGroup, networkInterface.Data.Id, "vm0000");
            //Thread.Sleep(30*60*1000); // Sleep 30 mins, wait for vm auto connect

            // GetAll
            var list = await DefaultSubscription.GetAllowedConnectionsResourcesAsync().ToEnumerableAsync();
            foreach (var item in list)
            {
                Console.WriteLine(item.Data.Id);
            }
            Console.WriteLine(list.Count);

            // Get
            var allowedConnections = await allowedConnectionsResourceCollection.GetAsync(AzureLocation.CentralUS, ConnectionType.Internal);
            Console.WriteLine(allowedConnections.Value.Data.Id);
        }

        [Test]
        public async Task AutomationOperation()
        {
            var collection = _resourceGroup.GetAutomations();

            // prerequisites
            //var workflow = await CreateLogicWorkFlow(_resourceGroup);


            // GetAll
            var list = await collection.GetAllAsync().ToEnumerableAsync();
            Console.WriteLine(list.Count);
        }

        [Test]
        public async Task ComplianceResult()
        {
            var collection = Client.GetComplianceResults(DefaultSubscription.Id);

            var response = await collection.GetAsync("DesignateMoreThanOneOwner");
            Console.WriteLine(response);

            // issue: 21144
            //var list = await collection.GetAllAsync().ToEnumerableAsync();
            //Console.WriteLine(list.Count);
        }

        [Test]
        public async Task ConnectSetting()
        {
            var collection = DefaultSubscription.GetConnectorSettings();

            // create
            string connectionSettingName = "connectionSetting0000";
            var data = new ConnectorSettingData()
            {

            };
            var connectionSetting = await collection.CreateOrUpdateAsync(WaitUntil.Completed, connectionSettingName, data);

            var list = await collection.GetAllAsync().ToEnumerableAsync();
            Console.WriteLine(list.Count);
        }

        [Test]
        public async Task IotSecurity()
        {
            string solutionName = "solution0000";
            IotSecuritySolutionModelData data = new IotSecuritySolutionModelData(_resourceGroup.Data.Location)
            {

            };
            //var newSolution  =  await _resourceGroup.GetIotSecuritySolutionModels().CreateOrUpdateAsync(WaitUntil.Completed, solutionName,data);

            var solutionlist = await _resourceGroup.GetIotSecuritySolutionModels().GetAllAsync().ToEnumerableAsync();

            var iotModelList = await DefaultSubscription.GetIotSecuritySolutionModelsAsync().ToEnumerableAsync();
            Console.WriteLine(iotModelList.First().Data.Id);
            Console.WriteLine();

            var xx = iotModelList.First().GetIotSecuritySolutionAnalyticsModel();

            var alertCollection = xx.GetIotSecurityAggregatedAlerts();
            var alertList = await alertCollection.GetAllAsync().ToEnumerableAsync();
            Console.WriteLine(alertList.Count);
            Console.WriteLine();

            var recommendationList = await xx.GetIotSecurityAggregatedRecommendations().GetAllAsync().ToEnumerableAsync();
            Console.WriteLine(recommendationList.Count);
            //Console.WriteLine(xx.Id);
            //Console.WriteLine(xx.Data.Name);
        }

        [Test]
        public async Task SecureScoreItems()
        {
            var collection = DefaultSubscription.GetSecureScoreItems();
            var list = await collection.GetAllAsync().ToEnumerableAsync();
            Console.WriteLine(list);
        }

        [Test]
        public async Task SecurityConnector()
        {
            var collection = _resourceGroup.GetSecurityConnectors();

            // create
            string securityConnectorName = "securityConnector0000";
            SecurityConnectorData data = new SecurityConnectorData(_resourceGroup.Data.Location)
            {
                Offerings =
                {
                    new CspmMonitorAwsOffering()
                    {
                        CloudRoleArn  =  "arn:aws:iam::00000000:role/ASCMonitor",
                    }
                },
                EnvironmentName = "AWS",
                EnvironmentData = new AWSEnvironmentData(),
                HierarchyIdentifier = "exampleHierarchyId",
            };
            var securityConnector = await collection.CreateOrUpdateAsync(WaitUntil.Completed, securityConnectorName, data);

            // getall
            var list = await collection.GetAllAsync().ToEnumerableAsync();
            Console.WriteLine(list.Count);
        }

        [Test]
        public async Task SecuritySolutions()
        {
            var list = await DefaultSubscription.GetSecuritySolutionsAsync().ToEnumerableAsync();
            Console.WriteLine(list.Count);
        }

        [Test]
        public async Task Software()
        {
            // prerequisites
            var vnet = await CreateDefaultNetwork(_resourceGroup, "vnet0000");
            var networkInterface = await CreateDefaultNetworkInterface(_resourceGroup, vnet, "networkInterface0000");
            var vm = await CreateDefaultVirtualMachine(_resourceGroup, networkInterface.Data.Id, "vm0000");

            var collection = _resourceGroup.GetSoftwares("Microsoft.Compute", "virtualMachines", vm.Data.Name);

            // getall
            var list = await collection.GetAllAsync().ToEnumerableAsync();
            Console.WriteLine(list.Count);
        }
    }
}
