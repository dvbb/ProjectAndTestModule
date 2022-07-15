using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using AzureSample;
using NUnit.Framework;

namespace Track2
{
    public class Tests : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Attention:
        ///     Referenced SDK [Azure.Resourcemanager.Network] is beta version.
        ///     The version of the [Azure.Resourcemanager] on which the [Azure.Resourcemanager.Network] depends may not match it,
        ///     therefore the SDK maybe do not work.
        /// </summary>
        /// <returns></returns>
        [Test]
        [Ignore("Need to investigate how to create a vnet")]
        public async Task Network_Create()
        {
            string rgName = "Network-RG-0000";
            string networkName = "network-0000";

            // Create ArmClient
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            ArmClient armClient = new ArmClient(clientSecretCredential, subscription);

            // Create a resource group
            ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
            ResourceGroupData rgData = new ResourceGroupData(AzureLocation.EastUS){};
            var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
            ResourceGroupResource resourceGroup = rgLro.Value;
            Assert.IsNotNull(resourceGroup);
            Assert.AreEqual(rgName, resourceGroup.Data.Name);

            // Create network
            var vnetName = "vnet-0000";
            VirtualNetworkData data = new VirtualNetworkData()
            {
                Location = resourceGroup.Data.Location,
            };
            data.AddressPrefixes.Add("10.10.0.0/16");
            data.Subnets.Add(new SubnetData() { Name = "subnet1", AddressPrefix = "10.10.1.0/24" });
            data.Subnets.Add(new SubnetData() { Name = "subnet2", AddressPrefix = "10.10.2.0/24" });
            var vnet = await resourceGroup.GetVirtualNetworks().CreateOrUpdateAsync(WaitUntil.Completed, vnetName, data);

            Assert.IsNotNull(vnet);
            Assert.AreEqual(networkName, vnet.Value.Data.Name);
        }
    }
}