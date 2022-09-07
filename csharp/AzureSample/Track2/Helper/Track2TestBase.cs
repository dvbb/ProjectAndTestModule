using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.KeyVault;
using Azure.ResourceManager.KeyVault.Models;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using Azure.ResourceManager.Storage.Models;
using Microsoft.Rest;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Track2.Helper
{
    public class Track2TestBase
    {
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
            VirtualNetworkData data = new VirtualNetworkData()
            {
                Location = resourceGroup.Data.Location,
            };
            data.AddressPrefixes.Add("10.10.0.0/16");
            data.Subnets.Add(new SubnetData() { Name = "subnet1", AddressPrefix = "10.10.1.0/24" });
            data.Subnets.Add(new SubnetData() { Name = "subnet2", AddressPrefix = "10.10.2.0/24" });
            data.Subnets.Add(new SubnetData() { Name = "subnet3", AddressPrefix = "10.10.3.0/24" });
            data.Subnets.Add(new SubnetData() { Name = "subnet4", AddressPrefix = "10.10.4.0/24" });
            data.Subnets.Add(new SubnetData() { Name = "subnet5", AddressPrefix = "10.10.5.0/24" });
            data.Subnets[0].Delegations.Add(new ServiceDelegation() { Name = "integrationServiceEnvironments", ServiceName = "Microsoft.Logic/integrationServiceEnvironments" });
            var vnet = await resourceGroup.GetVirtualNetworks().CreateOrUpdateAsync(WaitUntil.Completed, vnetName, data);
            return vnet.Value;
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
    }
}
