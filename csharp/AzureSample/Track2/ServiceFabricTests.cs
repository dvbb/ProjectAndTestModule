using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using AzureSample;
using Azure.ResourceManager.ServiceFabric;
using Azure.ResourceManager.KeyVault;
using Azure.ResourceManager.KeyVault.Models;
using NUnit.Framework;
using System;

namespace Track2
{
    internal class ServiceFabricTests : TestBase
    {
        private ResourceGroupResource _resourceGroup;

        [OneTimeSetUp]
        public async Task GlobalSetUp()
        {
            // Create ArmClient
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            ArmClient armClient = new ArmClient(clientSecretCredential, subscription);

            // Create a resource group
            string rgName = "ServiceFabric-RG-0000";
            ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
            ResourceGroupData rgData = new ResourceGroupData(AzureLocation.WestUS2) { };
            var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
            _resourceGroup = rgLro.Value;
        }

        [Test]
        public async Task KeyVault()
        {
            var collection = _resourceGroup.GetKeyVaults();

            string vaultName = "myVault0000";
            Guid tenantIdGuid = new Guid(tenantId);
            string objectId = "Your Object Id";
            IdentityAccessPermissions permissions = new IdentityAccessPermissions
            {
                Keys = { new IdentityAccessKeyPermission("all") },
                Secrets = { new IdentityAccessSecretPermission("all") },
                Certificates = { new IdentityAccessCertificatePermission("all") },
                Storage = { new IdentityAccessStoragePermission("all") },
            };
            KeyVaultAccessPolicy AccessPolicy = new KeyVaultAccessPolicy(tenantIdGuid, objectId, permissions);

            KeyVaultProperties VaultProperties = new KeyVaultProperties(tenantIdGuid, new KeyVaultSku(KeyVaultSkuFamily.A, KeyVaultSkuName.Standard));
            VaultProperties.EnabledForDeployment = true;
            VaultProperties.EnabledForDiskEncryption = true;
            VaultProperties.EnabledForTemplateDeployment = true;
            VaultProperties.EnableSoftDelete = true;
            VaultProperties.VaultUri = new Uri("http://vaulturi.com");
            VaultProperties.NetworkRuleSet = new KeyVaultNetworkRuleSet()
            {
                Bypass = "AzureServices",
                DefaultAction = "Allow",
                IPRules =
                {
                    new KeyVaultIPRule("1.2.3.4/32"),
                    new KeyVaultIPRule("1.0.0.0/25")
                }
            };
            VaultProperties.AccessPolicies.Add(AccessPolicy);

            KeyVaultCreateOrUpdateContent parameters = new KeyVaultCreateOrUpdateContent(AzureLocation.WestUS, VaultProperties);
            var keyvault = await collection.CreateOrUpdateAsync(WaitUntil.Completed, vaultName, parameters);
        }

        [Test]
        public async Task Cluster_E2E()
        {
            var collection = _resourceGroup.GetServiceFabricClusters();

            //string clusterName = "cluster0000";
            //ServiceFabricClusterData data = new ServiceFabricClusterData(_resourceGroup.Data.Location)
            //{
            //};
            //ClusterNodeTypeDescription clusterNodeTypeDescription = new ClusterNodeTypeDescription();
            //data.NodeTypes.Add(clusterNodeTypeDescription);
            //var cluster = await collection.CreateOrUpdateAsync(WaitUntil.Completed, clusterName, data);


            //var xx = await collection.GetAsync("newcluster0000");
            //Console.WriteLine(xx.Value.Data.Id);

            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Id);
            }
        }
    }
}
