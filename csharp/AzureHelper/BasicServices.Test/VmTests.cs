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
            _rgName = BaseClientExtension.CreateRandomName("VMRG");
            BaseClient baseClient = new BaseClient();
            resourceGroup = await baseClient.CreateResourceGroup(_rgName);
        }

        [TearDown]
        public async Task TearDown()
        {
            resourceGroup.Delete(WaitUntil.Started);
        }

        private async Task CreateVmPrepare()
        {
            // Create a NIC
            var vnet = await NetworkHelper.CreateVirtualNetwork(resourceGroup);
            var publicIP = await NetworkHelper.CreatePublicIP(resourceGroup);
            NetworkInterfaceResource nic = await NetworkHelper.CreateNetworkInterface(resourceGroup, vnet.Data.Subnets[0].Id, publicIP.Id);

            // Initialization
            _nicId = nic.Id;
            _vmName = BaseClientExtension.CreateRandomName("vm");
        }

        [Test]
        public async Task Linux_Pwd()
        {
            await CreateVmPrepare();

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
            await CreateVmPrepare();

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

        [Test]
        public async Task Disk()
        {
            string diskName = BaseClientExtension.CreateRandomName("disk");
            ManagedDiskData diskInput1 = new ManagedDiskData(resourceGroup.Data.Location)
            {
                Sku = new DiskSku()
                {
                    Name = DiskStorageAccountType.PremiumLrs
                },
                Zones = { "1" }, // Zone must be bigger than 0; otherwise, it cannot be attached to the VM
                CreationData = new DiskCreationData(DiskCreateOption.Empty),
                DiskSizeGB = 1024,
                DiskMBpsReadWrite = 200,
                Encryption = new DiskEncryption() { EncryptionType = ComputeEncryptionType.EncryptionAtRestWithPlatformKey },
                PublicNetworkAccess = DiskPublicNetworkAccess.Enabled,
            };
            var diskLro = await resourceGroup.GetManagedDisks().CreateOrUpdateAsync(WaitUntil.Completed, diskName, diskInput1);
            ManagedDiskResource disk = diskLro.Value;
            await Console.Out.WriteLineAsync(disk.Data.Name);
        }
    }
}
