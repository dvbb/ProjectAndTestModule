using Azure.ResourceManager.Network;
using Azure;
using Azure.ResourceManager.Resources;
using AzureHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Azure.ResourceManager.Network.Models;
using BasicServices.Tests.Helper;

namespace BasicServices.Tests
{
    internal class VnetTests
    {
        private ResourceGroupResource resourceGroup { get; set; }
        private string _rgName;

        [SetUp]
        public async Task SetUp()
        {
            _rgName = BaseClientExtension.CreateRandomName("NetworkRG");
            BaseClient baseClient = new BaseClient();
            resourceGroup = await baseClient.CreateResourceGroup(_rgName);
        }

        [TearDown]
        public async Task TearDown()
        {
            resourceGroup.Delete(WaitUntil.Started);
        }

        [Test]
        public async Task VirtualNetwork()
        {
            VirtualNetworkResource vnet = await NetworkHelper.CreateVirtualNetwork(resourceGroup);
            await Console.Out.WriteLineAsync(vnet.Data.Name);
        }

        [Test]
        public async Task NetworkInterface()
        {
            // prequisites
            var vnet = await NetworkHelper.CreateVirtualNetwork(resourceGroup);
            var publicIP = await NetworkHelper.CreatePublicIP(resourceGroup);

            ResourceIdentifier subnetId = vnet.Data.Subnets[0].Id;
            ResourceIdentifier publicIpId = publicIP.Id;

            NetworkInterfaceResource nic = await NetworkHelper.CreateNetworkInterface(resourceGroup, subnetId, publicIpId);
            await Console.Out.WriteLineAsync(nic.Data.Name);
        }

        [Test]
        public async Task PublicIP()
        {
            PublicIPAddressResource publicIP = await NetworkHelper.CreatePublicIP(resourceGroup);
            await Console.Out.WriteLineAsync(publicIP.Data.Name);
        }
    }
}
