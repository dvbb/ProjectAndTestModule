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

namespace Track2
{
    internal class SecurityCenterTests : Track2TestBase
    {
        private ResourceGroupResource _resourceGroup;

        [SetUp]
        public async Task TestSetUp()
        {
            _resourceGroup = await CreateResourceGroup("SecurityCenterRG-0000", AzureLocation.EastUS);
            //var vnet = await CreateDefaultNetwork(_resourceGroup, "vnet0000");
            //var networkInterface = await CreateDefaultNetworkInterface(_resourceGroup, vnet, "networkInterface0000");
            //var vm = await CreateDefaultVirtualMachine(_resourceGroup, networkInterface.Data.Id, "vm0000");
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

            Console.WriteLine(DateTime.Now);
            //Thread.Sleep(30*60*1000); // Sleep 30 mins, wait for vm auto connect
            Console.WriteLine(DateTime.Now);

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

            // GetAll
            var list = await collection.GetAllAsync().ToEnumerableAsync();
            Console.WriteLine(list.Count);
        }
    }
}
