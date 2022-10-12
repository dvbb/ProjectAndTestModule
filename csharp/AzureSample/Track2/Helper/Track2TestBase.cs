using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.KeyVault;
using Azure.ResourceManager.KeyVault.Models;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using Azure.ResourceManager.Storage.Models;
using Microsoft.Rest;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track2.Helper
{
    public class Track2TestBase
    {
        private const string dummySSHKey = "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQC+wWK73dCr+jgQOAxNsHAnNNNMEMWOHYEccp6wJm2gotpr9katuF/ZAdou5AaW1C61slRkHRkpRRX9FA9CYBiitZgvCCz+3nWNN7l/Up54Zps/pHWGZLHNJZRYyAB6j5yVLMVHIHriY49d/GZTZVNB8GoJv9Gakwc/fuEZYYl4YDFiGMBP///TzlI4jhiJzjKnEvqPFki5p2ZRJqcbCiF4pJrxUQR/RXqVFQdbRLZgYfJ8xGB878RENq3yQ39d8dVOkq4edbkzwcUmwwwkYVPIoDGsYLaRHnG+To7FvMeyO7xDVQkMKzopTQV8AuKpyvpqu0a9pWOMaiCyDytO7GGN you@me.com";
        protected string clientId => Environment.GetEnvironmentVariable("CLIENT_ID");
        protected string clientSecret => Environment.GetEnvironmentVariable("CLIENT_SECRET");
        protected string tenantId => Environment.GetEnvironmentVariable("TENANT_ID");
        protected string subscription => Environment.GetEnvironmentVariable("SUBSCRIPTION_ID");

        protected AzureLocation DefaultLocation = AzureLocation.EastUS;

        protected Track2TestBase()
        {
            CheckEffective(clientId);
            CheckEffective(clientSecret);
            CheckEffective(tenantId);
            CheckEffective(subscription);
        }

        protected void CheckEffective(string value)
        {
            if (value == null || string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException($"argument cannot be null.please make sure all enviroment parameters are correct.");
            }
        }

        protected async Task<string> GetToken()
        {
            // Get AccessToken with Azure.Identity
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            string[] scopes = { "https://management.core.windows.net/.default" };
            TokenRequestContext tokenRequestContext = new TokenRequestContext(scopes, "");
            var response = await clientSecretCredential.GetTokenAsync(tokenRequestContext);
            return response.Token;
        }

        protected async Task<ServiceClientCredentials> GetDefaultCredentialAsync()
        {
            string accessToken = await GetToken();

            // Get a existing an ADF pipeline
            TokenCredentials bauthCredentials = new TokenCredentials(accessToken);
            return bauthCredentials;
        }

        protected string GetRandomNumber(string resource)
        {
            Random random = new Random();
            return $"{resource}{random.Next(9999)}";
        }

        protected async Task<ResourceGroupResource> CreateResourceGroup(string resourceGroupName, AzureLocation location)
        {
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            ArmClient armClient = new ArmClient(clientSecretCredential, subscription);
            ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
            var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, resourceGroupName, new ResourceGroupData(location));
            return rgLro.Value;
        }

        protected async Task<VirtualNetworkResource> CreateDefaultNetwork(ResourceGroupResource resourceGroup, string vnetName)
        {
            // Create NSG
            string nsgName = "nsg1245";
            var nsgData = new NetworkSecurityGroupData() { Location = resourceGroup.Data.Location, };
            var nsg = await resourceGroup.GetNetworkSecurityGroups().CreateOrUpdateAsync(WaitUntil.Completed, nsgName, nsgData);

            // Create Network
            VirtualNetworkData data = new VirtualNetworkData()
            {
                Location = resourceGroup.Data.Location,
            };
            data.AddressPrefixes.Add("10.10.0.0/16");
            data.Subnets.Add(new SubnetData() { Name = "subnet1", AddressPrefix = "10.10.1.0/24", PrivateLinkServiceNetworkPolicy = VirtualNetworkPrivateLinkServiceNetworkPolicy.Disabled, NetworkSecurityGroup = nsg.Value.Data });
            data.Subnets.Add(new SubnetData() { Name = "subnet2", AddressPrefix = "10.10.2.0/24" });
            var vnet = await resourceGroup.GetVirtualNetworks().CreateOrUpdateAsync(WaitUntil.Completed, vnetName, data);
            return vnet.Value;
        }

        protected async Task<NetworkInterfaceResource> CreateDefaultNetworkInterface(ResourceGroupResource resourceGroup, VirtualNetworkResource network, string nicName)
        {
            // Create Public IP
            string publicIPName = "publicIP0000";
            var publicIPData = new PublicIPAddressData()
            {
                Location = resourceGroup.Data.Location,
                PublicIPAllocationMethod = NetworkIPAllocationMethod.Dynamic,
            };
            var publicIP = await resourceGroup.GetPublicIPAddresses().CreateOrUpdateAsync(WaitUntil.Completed, publicIPName, publicIPData);

            // Get subnet id AsyncPageable<SubnetResource>
            var list = await network.GetSubnets().GetAllAsync().ToEnumerableAsync();
            var subnetId = list.FirstOrDefault().Id;

            var data = new NetworkInterfaceData()
            {
                Location = resourceGroup.Data.Location,
                IPConfigurations =
                {
                    new NetworkInterfaceIPConfigurationData()
                    {
                        Name = "ipconfig0000",
                        PrivateIPAllocationMethod = NetworkIPAllocationMethod.Dynamic,
                        PublicIPAddress = new PublicIPAddressData()
                        {
                            Id = publicIP.Value.Id
                        },
                        Subnet = new SubnetData()
                        {
                            Id = subnetId
                        }
                    }
                }
            };
            var networkInterface = await resourceGroup.GetNetworkInterfaces().CreateOrUpdateAsync(WaitUntil.Completed, nicName, data);
            return networkInterface.Value;
        }

        protected async Task<KeyVaultResource> CreateDefaultKeyVault(ResourceGroupResource resourceGroup, string keyvaultName)
        {
            KeyVaultSku sku = new KeyVaultSku(KeyVaultSkuFamily.A, KeyVaultSkuName.Standard);
            KeyVaultProperties properties = new KeyVaultProperties(new Guid(tenantId), sku);
            KeyVaultCreateOrUpdateContent data = new KeyVaultCreateOrUpdateContent(resourceGroup.Data.Location, properties);
            var keyvault = await resourceGroup.GetKeyVaults().CreateOrUpdateAsync(WaitUntil.Completed, keyvaultName, data);
            return keyvault.Value;
        }

        protected async Task<StorageAccountResource> CreateDefaultStorage(ResourceGroupResource resourceGroup, string storageAccountName)
        {
            StorageSku sku = new StorageSku(StorageSkuName.StandardGrs);
            StorageKind kind = StorageKind.Storage;
            var location = resourceGroup.Data.Location;
            StorageAccountCreateOrUpdateContent storagedata = new StorageAccountCreateOrUpdateContent(sku, kind, location)
            {
                //AccessTier = StorageAccountAccessTier.Hot,
            };
            var storage = await resourceGroup.GetStorageAccounts().CreateOrUpdateAsync(WaitUntil.Completed, storageAccountName, storagedata);
            return storage.Value;
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
