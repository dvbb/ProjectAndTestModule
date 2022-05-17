using Azure.Core;
using Azure.Identity;
using AzureSample;
using Microsoft.Azure.Management.ContainerInstance;
using Microsoft.Azure.Management.ContainerInstance.Models;
using Microsoft.Rest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track1
{
    internal class ContainerInstanceTests : TestBase
    {
        [Test]
        public async Task ListCapabilities()
        {
            // Get region capabilities
            ServiceClientCredentials credentials = await GetDefaultCredentialAsync();
            ContainerInstanceManagementClient client = new ContainerInstanceManagementClient(credentials);
            client.SubscriptionId = subscription;
            var list1 = (await client.Location.ListCapabilitiesAsync("ukwest")).ToList();
            var list2 = (await client.Location.ListCapabilitiesAsync("eastus")).ToList();
            Console.WriteLine($"Gpu\t\tIpAddressType\tLocation\tOsType\t\tResourceType");
            foreach (var item in list1)
            {
                Console.WriteLine($"{item.Gpu}\t{item.IpAddressType}\t\t\t{item.Location}\t\t{item.OsType}\t{item.ResourceType}");
            }
            Console.WriteLine();
            foreach (var item in list2)
            {
                Console.WriteLine($"{item.Gpu}\t{item.IpAddressType}\t\t\t{item.Location}\t\t{item.OsType}\t{item.ResourceType}");
            }
        }

        [Test]
        public async Task CreateContainerInstanceGroup()
        {
            // Create a Container Instance
            ServiceClientCredentials credentials = await GetDefaultCredentialAsync();
            ContainerInstanceManagementClient client = new ContainerInstanceManagementClient(credentials);
            client.SubscriptionId = subscription; 
            string rgName = "ContainerInstance-RG-0000";
            string containerGroupName = GetRandomNumber("eastus-containerinstance");
            var aci = await client.ContainerGroups.CreateOrUpdateAsync(rgName, containerGroupName, new ContainerGroup()
            {
                Location = "eastus",
                Containers = new List<Container> {
                    new Container() {
                        Name = containerGroupName,
                        Ports = new List<ContainerPort>{ new ContainerPort { Protocol = "TCP", Port = 80} },
                        Image = "mcr.microsoft.com/azuredocs/aci-helloworld:latest",
                        Resources = new ResourceRequirements
                        {
                            Requests = new ResourceRequests
                            {
                                MemoryInGB = 1,
                                Cpu = 1,
                                Gpu =  new GpuResource
                                {
                                    Count = 1,
                                    Sku = "k80"
                                }
                            }
                        },
                    }
                },
                OsType = "Linux"
            });
            Console.WriteLine(aci.Id);
        }
    }
}
