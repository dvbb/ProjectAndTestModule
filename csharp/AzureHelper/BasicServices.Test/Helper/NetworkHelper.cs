using Azure.ResourceManager.Network;
using Azure.ResourceManager.Resources;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Azure.ResourceManager.Network.Models;

namespace BasicServices.Tests.Helper
{
    internal static class NetworkHelper
    {
        private static string CreateRandomName(string name) => AzureHelper.BaseClientExtension.CreateRandomName(name);

        public static async Task<VirtualNetworkResource> CreateVirtualNetwork(ResourceGroupResource resourceGroup, string vnetName = null)
        {
            vnetName = string.IsNullOrEmpty(vnetName) ? CreateRandomName("vnet") : vnetName;

            await Console.Out.WriteLineAsync("Creating virtual network...");
            VirtualNetworkData vnetInput = new VirtualNetworkData()
            {
                Location = resourceGroup.Data.Location,
                AddressPrefixes = { "10.10.0.0/16" },
                Subnets =
                    {
                        new SubnetData() { Name = "subnet1", AddressPrefix = "10.10.1.0/24"},
                        new SubnetData() { Name = "subnet2", AddressPrefix = "10.10.2.0/24"},
                    },
            };
            var vnetLro = await resourceGroup.GetVirtualNetworks().CreateOrUpdateAsync(WaitUntil.Completed, vnetName, vnetInput);
            await Console.Out.WriteLineAsync($"Created a virtual network: {vnetLro.Value.Data.Name}");
            return vnetLro.Value;
        }

        public static async Task<NetworkInterfaceResource> CreateNetworkInterface(ResourceGroupResource resourceGroup, ResourceIdentifier subnetId, ResourceIdentifier publicIpId, string nicName = null)
        {
            nicName = string.IsNullOrEmpty(nicName) ? CreateRandomName("nic") : nicName;

            await Console.Out.WriteLineAsync($"Creating network interface...");
            var nicInput = new NetworkInterfaceData()
            {
                Location = resourceGroup.Data.Location,
                IPConfigurations =
                    {
                        new NetworkInterfaceIPConfigurationData()
                        {
                            Name = "default-config",
                            PrivateIPAllocationMethod = NetworkIPAllocationMethod.Dynamic,
                            Subnet = new SubnetData()
                            {
                                Id = subnetId
                            },
                            PublicIPAddress = new PublicIPAddressData()
                            {
                                Id  = publicIpId
                            }
                        }
                    }
            };
            var networkInterfaceLro = await resourceGroup.GetNetworkInterfaces().CreateOrUpdateAsync(WaitUntil.Completed, nicName, nicInput);
            await Console.Out.WriteLineAsync($"Created network interface: {networkInterfaceLro.Value.Data.Name}");
            return networkInterfaceLro.Value;
        }

        public static async Task<PublicIPAddressResource> CreatePublicIP(ResourceGroupResource resourceGroup, string publicIPName = null)
        {
            publicIPName = string.IsNullOrEmpty(publicIPName) ? CreateRandomName("pip") : publicIPName;

            await Console.Out.WriteLineAsync("Creating a public IP address...");
            PublicIPAddressData publicIPInput = new PublicIPAddressData()
            {
                Location = resourceGroup.Data.Location,
                Sku = new PublicIPAddressSku()
                {
                    Name = PublicIPAddressSkuName.Standard,
                    Tier = PublicIPAddressSkuTier.Regional
                },
                PublicIPAllocationMethod = NetworkIPAllocationMethod.Static,
                DnsSettings = new PublicIPAddressDnsSettings { DomainNameLabel = publicIPName },
            };
            var publicIPLro = await resourceGroup.GetPublicIPAddresses().CreateOrUpdateAsync(WaitUntil.Completed, publicIPName, publicIPInput);
            PublicIPAddressResource publicIP = publicIPLro.Value;
            await Console.Out.WriteLineAsync($"Created a public IP address: {publicIPLro.Value.Data.Name}");
            return publicIPLro.Value;
        }
    }
}
