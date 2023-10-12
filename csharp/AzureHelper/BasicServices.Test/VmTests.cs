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
using Azure.ResourceManager.Compute;
using Microsoft.Extensions.Azure;
using Azure.ResourceManager.Compute.Models;
using System.Net.NetworkInformation;

namespace BasicServices.Tests
{
    internal class VmTests
    {
        private ResourceGroupResource resourceGroup { get; set; }
        ResourceIdentifier _nicId;
        private string _rgName;
        private string _vmName;

        [SetUp]
        public async Task SetUp()
        {
            // Create a resource group
            _rgName = BaseClientExtension.CreateRandomName("vm");
            BaseClient baseClient = new BaseClient();
            resourceGroup = await baseClient.CreateResourceGroup(_rgName);

            // Create a NIC
            var vnet = await NetworkHelper.CreateVirtualNetwork(resourceGroup);
            var publicIP = await NetworkHelper.CreatePublicIP(resourceGroup);
            NetworkInterfaceResource nic = await NetworkHelper.CreateNetworkInterface(resourceGroup, vnet.Data.Subnets[0].Id, publicIP.Id);

            // Initialization
            _nicId = nic.Id;
            _vmName = BaseClientExtension.CreateRandomName("vm");
        }

        [TearDown]
        public async Task TearDown()
        {
            resourceGroup.Delete(WaitUntil.Started);
        }

        [Test]
        public async Task Linux_Pwd()
        {
            VirtualMachineData vmInput = VirtualMachineHelper.GetInputData_Linux_Pwd(resourceGroup, _vmName);
            vmInput.NetworkProfile.NetworkInterfaces.Add(
                new VirtualMachineNetworkInterfaceReference()
                {
                    Id = _nicId,
                    Primary = true,
                });
            var vmLro = await resourceGroup.GetVirtualMachines().CreateOrUpdateAsync(WaitUntil.Completed, _vmName, vmInput);
            VirtualMachineResource vm = vmLro.Value;
            await Console.Out.WriteLineAsync(vm.Data.Name);
            await Console.Out.WriteLineAsync(vm.Data.StorageProfile.OSDisk.OSType.ToString());
        }

        [Test]
        public async Task Windows()
        {
            VirtualMachineData vmInput = VirtualMachineHelper.GetInputData_Windows(resourceGroup, _vmName);
            vmInput.NetworkProfile.NetworkInterfaces.Add(
                new VirtualMachineNetworkInterfaceReference()
                {
                    Id = _nicId,
                    Primary = true,
                });
            var vmLro = await resourceGroup.GetVirtualMachines().CreateOrUpdateAsync(WaitUntil.Completed, _vmName, vmInput);
            VirtualMachineResource vm = vmLro.Value;
            await Console.Out.WriteLineAsync(vm.Data.Name);
            await Console.Out.WriteLineAsync(vm.Data.StorageProfile.OSDisk.OSType.ToString());
        }
    }
}
