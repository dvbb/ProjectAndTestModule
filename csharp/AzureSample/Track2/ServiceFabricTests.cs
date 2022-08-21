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
using Azure.ResourceManager.ServiceFabric.Models;

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
            ResourceGroupData rgData = new ResourceGroupData(AzureLocation.UKWest) { };
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
        public async Task Cluster_Create()
        {
            var collection = _resourceGroup.GetServiceFabricClusters();

            string clusterName = "cluster00001111";
            string certKey = "";
            ServiceFabricClusterData data = new ServiceFabricClusterData(_resourceGroup.Data.Location)
            {
                //Certificate = new ClusterCertificateDescription(new BinaryData(certKey)),
                ManagementEndpoint = new Uri("https://cluster00001111.ukwest.cloudapp.azure.com:19080/"),
            };
            //string name, int clientConnectionEndpointPort, int httpGatewayEndpointPort, bool isPrimary, int vmInstanceCount)
            ClusterNodeTypeDescription clusterNodeTypeDescription = new ClusterNodeTypeDescription("Type812", 19000, 19080, true, 5);
            data.NodeTypes.Add(clusterNodeTypeDescription);
            var cluster = await collection.CreateOrUpdateAsync(WaitUntil.Completed, clusterName, data);
            Console.WriteLine(cluster.Value.Data.Id);
        }

        [Test]
        public async Task Cluster_GetAll()
        {
            var collection = _resourceGroup.GetServiceFabricClusters();
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Id);
            }
        }
    }
}
