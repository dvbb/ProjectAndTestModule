using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Resources;
using AzureSample;
using NUnit.Framework;

namespace Track2
{
    internal class iothub : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        //private async Task<ResourceGroupResource> CreateDefaultResourceGroup()
        //{
        //    string rgName = "Iothub-RG-Custom-0000";
        //    ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        //    ArmClient armClient = new ArmClient(clientSecretCredential, subscription);
        //    ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
        //    ResourceGroupData rgData = new ResourceGroupData(AzureLocation.WestUS2) { };
        //    var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
        //    return rgLro.Value;
        //}

        //private async Task CreateDefaultNetwork(ResourceGroupResource resourceGroup)
        //{
        //    // Create network
        //    var vnetName = "vnet-0000";
        //    VirtualNetworkData data = new VirtualNetworkData()
        //    {
        //        Location = resourceGroup.Data.Location,
        //    };
        //    data.AddressPrefixes.Add("10.10.0.0/16");
        //    data.Subnets.Add(new SubnetData() { Name = "subnet1", AddressPrefix = "10.10.1.0/24" });
        //    data.Subnets.Add(new SubnetData() { Name = "subnet2", AddressPrefix = "10.10.2.0/24" });
        //    var vnet = await resourceGroup.GetVirtualNetworks().CreateOrUpdateAsync(WaitUntil.Completed, vnetName, data);
        //}

        //protected async Task<IotHubDescriptionResource> CreateDefaultIotHub(ResourceGroupResource resourceGroup)
        //{
        //    string iotHubName = "iothub-custom-0000";
        //    var sku = new IotHubSkuInfo("S1")
        //    {
        //        Name = "S1",
        //        Capacity = 1
        //    };
        //    IotHubDescriptionData data = new IotHubDescriptionData(resourceGroup.Data.Location, sku) { };
        //    var iotHub = await resourceGroup.GetIotHubDescriptions().CreateOrUpdateAsync(WaitUntil.Completed, iotHubName, data);
        //    return iotHub.Value;
        //}

        //[Test]
        //public async Task IotHub_Delete()
        //{
        //    ResourceGroupResource resourceGroup = await CreateDefaultResourceGroup();
        //    var iothub = await CreateDefaultIotHub(resourceGroup);

        //    Console.WriteLine("wait..");
        //    await iothub.DeleteAsync(WaitUntil.Completed);
        //}

        //[Test]
        //public async Task PrivateLink_GetAll()
        //{
        //    ResourceGroupResource resourceGroup = await CreateDefaultResourceGroup();
        //    var iothub = await CreateDefaultIotHub(resourceGroup);
        //    var collection = iothub.GetIotHubPrivateEndpointConnections();

        //    Console.WriteLine("wait..");
        //    await foreach (IotHubPrivateEndpointConnectionResource item in collection.GetAllAsync())
        //    {
        //        Console.WriteLine(item.Data.Name);
        //    }
        //}

        //[Test]
        //public async Task PrivateLink_Delete()
        //{
        //    ResourceGroupResource resourceGroup = await CreateDefaultResourceGroup();
        //    await CreateDefaultNetwork(resourceGroup);
        //    var iothub = await CreateDefaultIotHub(resourceGroup);
        //    var collection = iothub.GetIotHubPrivateEndpointConnections();

        //    // System.InvalidOperationException : The requested operation requires an element of type 'Array', but the target element has type 'Object'.
        //    Console.WriteLine("wait..");
        //    string ConnectionName = "iothub-custom-0000.6181d27a-8b1c-4aaa-8fe6-6bf10257e304";
        //    var conn = await collection.GetAsync(ConnectionName);
        //    await conn.Value.DeleteAsync(WaitUntil.Completed);
        //}
    }
}
