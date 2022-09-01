//using System.Threading.Tasks;
//using Azure;
//using Azure.Core;
//using Azure.Identity;
//using Azure.ResourceManager;
//using Azure.ResourceManager.Resources;
//using AzureSample;
//using Azure.ResourceManager.ServiceFabric;
//using Azure.ResourceManager.KeyVault;
//using Azure.ResourceManager.KeyVault.Models;
//using NUnit.Framework;
//using System;
//using Azure.ResourceManager.ServiceFabric.Models;
//using System.Linq;

//namespace Track2
//{
//    internal class ServiceFabricTests : TestBase
//    {
//        private ResourceGroupResource _resourceGroup;
//        private ServiceFabricClusterResource _cluster;

//        [OneTimeSetUp]
//        public async Task GlobalSetUp()
//        {
//            // Create ArmClient
//            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
//            ArmClient armClient = new ArmClient(clientSecretCredential, subscription);

//            // Create a resource group
//            string rgName = "ServiceFabric-RG-0000";
//            ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
//            ResourceGroupData rgData = new ResourceGroupData(AzureLocation.UKWest) { };
//            var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
//            _resourceGroup = rgLro.Value;

//            // Create a Service Fabric Cluster
//            string clusterName = "cluster0000";
//            _cluster = await CreateServiceFabricCluster(clusterName);
//        }

//        private async Task<ServiceFabricClusterResource> CreateServiceFabricCluster(string clusterName)
//        {
//            ServiceFabricClusterData data = new ServiceFabricClusterData(_resourceGroup.Data.Location)
//            {
//                ManagementEndpoint = new Uri($"https://{clusterName}.{_resourceGroup.Data.Location.ToString()}.cloudapp.azure.com:19080/"),
//            };
//            ClusterNodeTypeDescription clusterNodeTypeDescription = new ClusterNodeTypeDescription("Type812", 19000, 19080, true, 5);
//            data.NodeTypes.Add(clusterNodeTypeDescription);
//            var cluster = await _resourceGroup.GetServiceFabricClusters().CreateOrUpdateAsync(WaitUntil.Completed, clusterName, data);
//            return cluster.Value;
//        }

//        [Test]
//        public async Task KeyVault()
//        {
//            var collection = _resourceGroup.GetKeyVaults();

//            string vaultName = "myVault0000";
//            Guid tenantIdGuid = new Guid(tenantId);
//            string objectId = "Your Object Id";
//            IdentityAccessPermissions permissions = new IdentityAccessPermissions
//            {
//                Keys = { new IdentityAccessKeyPermission("all") },
//                Secrets = { new IdentityAccessSecretPermission("all") },
//                Certificates = { new IdentityAccessCertificatePermission("all") },
//                Storage = { new IdentityAccessStoragePermission("all") },
//            };
//            KeyVaultAccessPolicy AccessPolicy = new KeyVaultAccessPolicy(tenantIdGuid, objectId, permissions);

//            KeyVaultProperties VaultProperties = new KeyVaultProperties(tenantIdGuid, new KeyVaultSku(KeyVaultSkuFamily.A, KeyVaultSkuName.Standard));
//            VaultProperties.EnabledForDeployment = true;
//            VaultProperties.EnabledForDiskEncryption = true;
//            VaultProperties.EnabledForTemplateDeployment = true;
//            VaultProperties.EnableSoftDelete = true;
//            VaultProperties.VaultUri = new Uri("http://vaulturi.com");
//            VaultProperties.NetworkRuleSet = new KeyVaultNetworkRuleSet()
//            {
//                Bypass = "AzureServices",
//                DefaultAction = "Allow",
//                IPRules =
//                {
//                    new KeyVaultIPRule("1.2.3.4/32"),
//                    new KeyVaultIPRule("1.0.0.0/25")
//                }
//            };
//            VaultProperties.AccessPolicies.Add(AccessPolicy);

//            KeyVaultCreateOrUpdateContent parameters = new KeyVaultCreateOrUpdateContent(AzureLocation.WestUS, VaultProperties);
//            var keyvault = await collection.CreateOrUpdateAsync(WaitUntil.Completed, vaultName, parameters);
//        }

//        [Test]
//        public async Task Cluster_Create()
//        {
//            var collection = _resourceGroup.GetServiceFabricClusters();

//            string clusterName = "cluster00001111";
//            string certKey = "";
//            ServiceFabricClusterData data = new ServiceFabricClusterData(_resourceGroup.Data.Location)
//            {
//                //Certificate = new ClusterCertificateDescription(new BinaryData(certKey)),
//                ManagementEndpoint = new Uri("https://cluster00001111.ukwest.cloudapp.azure.com:19080/"),
//            };
//            //string name, int clientConnectionEndpointPort, int httpGatewayEndpointPort, bool isPrimary, int vmInstanceCount)
//            ClusterNodeTypeDescription clusterNodeTypeDescription = new ClusterNodeTypeDescription("Type812", 19000, 19080, true, 5);
//            data.NodeTypes.Add(clusterNodeTypeDescription);
//            var cluster = await collection.CreateOrUpdateAsync(WaitUntil.Completed, clusterName, data);
//            Console.WriteLine(cluster.Value.Data.Id);
//        }

//        [Test]
//        public async Task Cluster_E2E_WithoutCreate()
//        {
//            var collection = _resourceGroup.GetServiceFabricClusters();

//            // GetAll
//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }

//            // Get Upgradable Versions
//            //var version = await _cluster.GetUpgradableVersionsAsync();
//            //Console.WriteLine(version);

//            // Get Locations
//            var locations = (await _cluster.GetAvailableLocationsAsync()).Value.ToArray();
//            Console.WriteLine(locations.Count());
//            foreach (var item in locations)
//            {
//                //Console.WriteLine(item);
//            }
//            Console.WriteLine(locations);
//        }

//        [Test]
//        public async Task Application_E2E()
//        {
//            var collection = _cluster.GetServiceFabricApplications();

//            // Create
//            //ServiceFabricApplicationData data = new ServiceFabricApplicationData(_cluster.Data.Location);
//            //var app =  await collection.CreateOrUpdateAsync(WaitUntil.Completed,"app0000",data);

//            // GetAll
//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }
//        }

//        [Test]
//        public async Task OutsideRG_GetApp()
//        {
//            // Create ArmClient
//            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
//            ArmClient armClient = new ArmClient(clientSecretCredential, subscription);

//            ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
//            var resourceGroup = await rgCollection.GetAsync("voting-rg");
//            var cluster = await resourceGroup.Value.GetServiceFabricClusters().GetAsync("voting");
//            var collection = cluster.Value.GetServiceFabricApplications();

//            Console.WriteLine(cluster.Value.Data.Id.ResourceGroupName);
//            Console.WriteLine($"{cluster.Value.Data.Id.ResourceType}:  {cluster.Value.Data.Id.Name}");

//            // GetAll
//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }

//            // Get. 404 notfound
//            //var app = await collection.GetAsync("Voting");
//            //Console.WriteLine(app.Value.Data.Name);

//            // Get though armclient. 404 notfound
//            //string id_str = "*************************";
//            //ResourceIdentifier id = new ResourceIdentifier(id_str);
//            //var app = await armClient.GetServiceFabricApplicationResource(id).GetAsync();
//            //Console.WriteLine(app.Value.Data.Name);
//        }
//    }
//}
