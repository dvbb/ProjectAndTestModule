using Azure.Core;
using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager;
using Azure;
using AzureHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitializationBasicServices
{
    internal class InitComputer
    {
        private const string dummySSHKey = "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQC+wWK73dCr+jgQOAxNsHAnNNNMEMWOHYEccp6wJm2gotpr9katuF/ZAdou5AaW1C61slRkHRkpRRX9FA9CYBiitZgvCCz+3nWNN7l/Up54Zps/pHWGZLHNJZRYyAB6j5yVLMVHIHriY49d/GZTZVNB8GoJv9Gakwc/fuEZYYl4YDFiGMBP///TzlI4jhiJzjKnEvqPFki5p2ZRJqcbCiF4pJrxUQR/RXqVFQdbRLZgYfJ8xGB878RENq3yQ39d8dVOkq4edbkzwcUmwwwkYVPIoDGsYLaRHnG+To7FvMeyO7xDVQkMKzopTQV8AuKpyvpqu0a9pWOMaiCyDytO7GGN you@me.com";
        public void Init() 
        { 
            BaseClient baseClient = new BaseClient();
            baseClient.CreateResourceGroup("");
        }

        protected async Task<VirtualMachineResource> CreateDefaultVirtualMachine(ResourceGroupResource resourceGroup, ResourceIdentifier networkInterfaceIP, string vmName)
        {
            string adminUsername = "adminUser";
            VirtualMachineCollection vmCollection = resourceGroup.GetVirtualMachines();
            VirtualMachineData input = new VirtualMachineData(resourceGroup.Data.Location)
            {
                HardwareProfile = new VirtualMachineHardwareProfile()
                {
                    VmSize = VirtualMachineSizeType.StandardF2
                },
                OSProfile = new VirtualMachineOSProfile()
                {
                    AdminUsername = adminUsername,
                    ComputerName = vmName,
                    LinuxConfiguration = new LinuxConfiguration()
                    {
                        DisablePasswordAuthentication = true,
                        SshPublicKeys = {
                            new SshPublicKeyConfiguration()
                            {
                                Path = $"/home/{adminUsername}/.ssh/authorized_keys",
                                KeyData = dummySSHKey,
                            }
                        }
                    }
                },
                NetworkProfile = new VirtualMachineNetworkProfile()
                {
                    NetworkInterfaces =
                    {
                        new VirtualMachineNetworkInterfaceReference()
                        {
                            Id = networkInterfaceIP,
                            Primary = true,
                        }
                    }
                },
                StorageProfile = new VirtualMachineStorageProfile()
                {
                    OSDisk = new VirtualMachineOSDisk(DiskCreateOptionType.FromImage)
                    {
                        OSType = SupportedOperatingSystemType.Linux,
                        Caching = CachingType.ReadWrite,
                        ManagedDisk = new VirtualMachineManagedDisk()
                        {
                            StorageAccountType = StorageAccountType.StandardLrs
                        }
                    },
                    ImageReference = new ImageReference()
                    {
                        Publisher = "Canonical",
                        Offer = "UbuntuServer",
                        Sku = "16.04-LTS",
                        Version = "latest",
                    }
                }
            };
            ArmOperation<VirtualMachineResource> lro = await vmCollection.CreateOrUpdateAsync(WaitUntil.Completed, vmName, input);
            return lro.Value;
        }
    }
}
