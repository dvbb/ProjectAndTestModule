using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Network;
using AzureSample;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Azure.ResourceManager.Network.Models;

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
            var rgLro = await rgCollection.CreateOrUpdateAsync(true, rgName, rgData);
            ResourceGroup resourceGroup = rgLro.Value;
            Assert.IsNotNull(resourceGroup);
            Assert.AreEqual(rgName, resourceGroup.Data.Name);

            // Create network
            VirtualNetworkData networkData = new VirtualNetworkData()
            {
                Location = "eastus",
                AddressSpace = new AddressSpace()
                {
                    AddressPrefixes = { "10.10.0.0/16", }
                },
                Subnets =
                {
                    new SubnetData() { Name = "subnet01", AddressPrefix = "10.10.1.0/24", },
                    new SubnetData() { Name = "subnet02", AddressPrefix = "10.10.2.0/24", PrivateEndpointNetworkPolicies = "Disabled", }
                },
            };
            var vnet = await resourceGroup.GetVirtualNetworks().CreateOrUpdateAsync(networkName,networkData);
            Assert.IsNotNull(vnet);
            Assert.AreEqual(networkName, vnet.Value.Data.Name);
        }
    }
}