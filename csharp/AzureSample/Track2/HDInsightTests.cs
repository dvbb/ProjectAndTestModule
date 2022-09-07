using Azure;
using Azure.Core;
using Azure.ResourceManager.HDInsight;
using Azure.ResourceManager.HDInsight.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Track2.Helper;

namespace Track2
{
    internal class HDInsightTests : Track2TestBase
    {
        private ResourceGroupResource _resourceGroup;
        private StorageAccountResource _storageAccount;
        private HDInsightClusterCollection _clusterCollection => _resourceGroup.GetHDInsightClusters();

        [OneTimeSetUp]
        public async Task GlobalSetUp()
        {
            string resourceGroupName = "HDInsightRG-0000";
            string storageAccountName = "storageacoount220905";
            _resourceGroup = await CreateResourceGroup(resourceGroupName, AzureLocation.EastUS);
            _storageAccount = await CreateDefaultStorage(_resourceGroup, storageAccountName);
            //_storageAccount = await _resourceGroup.GetStorageAccounts().GetAsync("cluster0000hdistorage ");
        }

        [Test]
        public void StringParse()
        {
            string str = null;
            bool flag = default;

            str = "true";
            flag = bool.Parse(str);

            str = "false";
            flag = bool.Parse(str);
        }

        [Test]
        public async Task Cluster_LinuxHadoopSshPassword()
        {
            // Create Storage Container 
            string containerName = "container1000";
            var container = await _storageAccount.GetBlobService().GetBlobContainers().CreateOrUpdateAsync(WaitUntil.Completed, containerName, new BlobContainerData());

            var collection = _resourceGroup.GetHDInsightClusters();
            string clusterName = "hadoopcluster1000000";
            string common_user = "sshuser5951";
            string common_passwork = "Password!5951";
            string key = (await _storageAccount.GetKeysAsync()).Value.Keys.FirstOrDefault().Value;
            string clusterDeifnitionConfigurations = "{         \"gateway\": {             \"restAuthCredential.isEnabled\": \"true\",             \"restAuthCredential.username\": \"admin4468\",             \"restAuthCredential.password\": \"Password1!9688\"         }     } ";
            var properties = new HDInsightClusterCreateOrUpdateProperties()
            {
                ClusterVersion = "3.6",
                OSType = HDInsightOSType.Linux,
                Tier = HDInsightTier.Standard,
                ClusterDefinition = new HDInsightClusterDefinition()
                {
                    Kind = "Hadoop",
                    Configurations = new BinaryData(clusterDeifnitionConfigurations),
                },
                IsEncryptionInTransitEnabled = true,
            };
            properties.ComputeRoles.Add(new HDInsightClusterRole()
            {
                Name = "headnode",
                TargetInstanceCount = 2,
                HardwareVmSize = "Large",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = common_user,
                    Password = common_passwork
                }
            });
            properties.ComputeRoles.Add(new HDInsightClusterRole()
            {
                Name = "workernode",
                TargetInstanceCount = 3,
                HardwareVmSize = "Large",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = common_user,
                    Password = common_passwork
                }
            });
            properties.ComputeRoles.Add(new HDInsightClusterRole()
            {
                Name = "zookeepernode",
                TargetInstanceCount = 3,
                HardwareVmSize = "Small",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = common_user,
                    Password = common_passwork
                }
            });
            properties.StorageAccounts.Add(new HDInsightStorageAccountInfo()
            {
                Name = $"{_storageAccount.Data.Name}.blob.core.windows.net",
                IsDefault = true,
                Container = container.Value.Data.Name,
                Key = key,
            });
            var data = new HDInsightClusterCreateOrUpdateContent()
            {
                Properties = properties,
                Location = _resourceGroup.Data.Location,
            };
            data.Tags.Add(new KeyValuePair<string, string>("key0", "value0"));
            var cluster = await collection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, clusterName, data);

            Console.WriteLine(cluster.Value.Data.Name);
            Console.WriteLine(cluster.Value.Data.Tags.Count);
            Console.WriteLine(cluster.Value.Data.Properties.OSType.ToString());
            Console.WriteLine(cluster.Value.Data.Properties.StorageAccounts.Count);
            Console.WriteLine(cluster.Value.Data.Properties.Tier.ToString());
            Console.WriteLine(cluster.Value.Data.Properties.IsEncryptionInTransitEnabled);
        }

        [Test]
        public async Task Cluster_Extension()
        {
            string clusterName = "hadoopcluster1000000";
            var clusterLro = await _resourceGroup.GetHDInsightClusters().GetAsync(clusterName);
            var cluster = clusterLro.Value;

            // get
            var extension = await cluster.GetExtensionAsync("azuremonitor");

            Console.WriteLine(extension);
            Console.WriteLine(extension.Value.IsClusterMonitoringEnabled);
            Console.WriteLine(extension.Value.WorkspaceId); // null

        }

        [Test]
        public async Task Cluster_GetAzureMonitorExtensionStatus()
        {
            string clusterName = "hadoopcluster1000000";
            var clusterLro = await _resourceGroup.GetHDInsightClusters().GetAsync(clusterName);
            var cluster = clusterLro.Value;

            // get
            var response = await cluster.GetAzureMonitorExtensionStatusAsync();

            Console.WriteLine(response.Value);
            Console.WriteLine(response.Value.IsClusterMonitoringEnabled);
            Console.WriteLine(response.Value.SelectedConfigurations); //null
            Console.WriteLine(response.Value.WorkspaceId); // null
        }

        [Test]
        public async Task Cluster_Application_Create()
        {
            string clusterName = "hadoopcluster1000000";
            var cluster = await _resourceGroup.GetHDInsightClusters().GetAsync(clusterName);

            var collection = cluster.Value.GetHDInsightApplications();
            string applicationName = "app200000";
            var properties = new HDInsightApplicationProperties() { };

            var uri = new Uri("https://hdiconfigactions.blob.core.windows.net/linuxhueconfigactionv02/install-hue-uber-v02.sh");
            var roles = new[] { "edgenode" };
            properties.InstallScriptActions.Add(new RuntimeScriptAction("InstallHue2", uri, roles)
            {
                Parameters = "-version latest -port 20000"
            });
            properties.ApplicationType = "CustomApplication";
            properties.ComputeRoles.Add(new HDInsightClusterRole()
            {
                Name = "edgenode",
                TargetInstanceCount = 1,
                HardwareVmSize = "Large"
            });

            var data = new HDInsightApplicationData()
            {
                Properties = properties,
            };
            data.Tags.Add("kyes", "values");
            var app = await collection.CreateOrUpdateAsync(WaitUntil.Completed, applicationName, data);

            Console.WriteLine(app.Value.Data.Name);
            Console.WriteLine(app.Value.Data.Properties.ApplicationState);
            Console.WriteLine(app.Value.Data.Properties.ApplicationType);
            Console.WriteLine(app.Value.Data.Properties.InstallScriptActions.Count);
            Console.WriteLine(app.Value.Data.Properties.ProvisioningState);
            Console.WriteLine(app.Value.Data.Properties.CreatedDate);
        }

        [Test]
        public async Task Cluster_Application_GetAll()
        {
            string clusterName = "hadoopcluster1000000";
            var cluster = await _resourceGroup.GetHDInsightClusters().GetAsync(clusterName);

            var collection = cluster.Value.GetHDInsightApplications();
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        [Ignore("Private link is not enabled for this cluster")]
        public async Task Cluster_PrivateConnect_GetAll()
        {
            string clusterName = "hadoopcluster1000000";
            var cluster = await _resourceGroup.GetHDInsightClusters().GetAsync(clusterName);

            var collection = cluster.Value.GetHDInsightPrivateEndpointConnections();
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public async Task Cluster_GetAll()
        {
            var collection = _resourceGroup.GetHDInsightClusters();

            // GetAll
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
                Console.WriteLine(item.Data.Properties.ClusterVersion);
                Console.WriteLine(item.Data.Properties.OSType.ToString());
                Console.WriteLine(item.Data.Properties.Tier.ToString());
                Console.WriteLine();
                Console.WriteLine(item.Data.Properties.StorageAccounts[0].Name.ToString());
                Console.WriteLine(item.Data.Properties.StorageAccounts[0].IsDefault.ToString());
                Console.WriteLine(item.Data.Properties.StorageAccounts[0].Container.ToString());
                Console.WriteLine();
            }
        }

        [Test]
        public async Task TestCreateClusterWithAutoScaleLoadBasedType()
        {
            var collection = _resourceGroup.GetHDInsightClusters();

            string clusterName = "hdisdk-premium";

            string containerName = "hdisdk-premium-container";
            var container = await _storageAccount.GetBlobService().GetBlobContainers().CreateOrUpdateAsync(WaitUntil.Completed, containerName, new BlobContainerData());
            string key = (await _storageAccount.GetKeysAsync()).Value.Keys.FirstOrDefault().Value;

            var properties = PrepareClusterCreateParams(_storageAccount.Data.Name, containerName, key);
            var workerNode = properties.ComputeRoles.First(role => role.Name.Equals("workernode"));

            //Add auto scale configuration Load-based type
            workerNode.AutoScaleConfiguration = new HDInsightAutoScaleConfiguration()
            {
                Capacity = new HDInsightAutoScaleCapacity()
                {
                    MinInstanceCount = 4,
                    MaxInstanceCount = 5
                }
            };

            var data = new HDInsightClusterCreateOrUpdateContent()
            {
                Properties = properties,
                Location = _resourceGroup.Data.Location,
            };
            data.Tags.Add(new KeyValuePair<string, string>("key0", "value0"));
            var cluster = await collection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, clusterName, data);
        }

        [Test]
        public async Task TestCreateClusterWithEncryptionInTransit()
        {
            // 200: AzureResourceCreationFailedErrorCode","message":"Internal server error occurred while processing the request. Please retry the request or contact support
            string clusterName = "hdisdk-encryption";
            var properties = await PrepareClusterCreateParams(_storageAccount);
            properties.ClusterDefinition.Kind = "Spark";
            properties.IsEncryptionInTransitEnabled = true;

            var data = new HDInsightClusterCreateOrUpdateContent()
            {
                Properties = properties,
                Location = _resourceGroup.Data.Location,
            };
            data.Tags.Add(new KeyValuePair<string, string>("key0", "value0"));
            var cluster = await _clusterCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, clusterName, data);
            Assert.IsNotNull(cluster);
        }

        [Test]
        public async Task TestCreateWithAdditionalStorageAccount()
        {
            string clusterName = "hdisdk-additional";
            var properties = await PrepareClusterCreateParams(_storageAccount);

            // Add additional storage account
            string secondaryStorageAccountName = "azstorageforcluster8000";
            string containerName = "container8000";
            var secondaryStorageAccount = await CreateDefaultStorage(_resourceGroup, secondaryStorageAccountName);
            string accessKey = (await secondaryStorageAccount.GetKeysAsync()).Value.Keys.FirstOrDefault().Value;
            await secondaryStorageAccount.GetBlobService().GetBlobContainers().CreateOrUpdateAsync(WaitUntil.Completed, containerName, new BlobContainerData());

            properties.StorageAccounts.Add(new HDInsightStorageAccountInfo()
            {
                Name = $"{secondaryStorageAccount.Data.Name}.blob.core.windows.net",
                IsDefault = false,
                Container = containerName,
                Key = accessKey,
            });

            var data = new HDInsightClusterCreateOrUpdateContent()
            {
                Properties = properties,
                Location = _resourceGroup.Data.Location,
            };
            data.Tags.Add(new KeyValuePair<string, string>("key0", "value0"));
            var cluster = await _clusterCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, clusterName, data);
            Assert.IsNotNull(cluster);
        }

        [Test]
        public async Task TestCreateWithEmptyExtendedParameters()
        {
            string clusterName = "hdisdk-empty";
            var data = new HDInsightClusterCreateOrUpdateContent()
            {
            };
            Assert.ThrowsAsync<RequestFailedException>(() => _clusterCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, clusterName, data));
        }

        private HDInsightClusterCreateOrUpdateProperties PrepareClusterCreateParams(string storageAccountName, string containerName, string accessKey)
        {
            string common_user = "sshuser5951";
            string common_passwork = "Password!5951";
            string clusterDeifnitionConfigurations = "{         \"gateway\": {             \"restAuthCredential.isEnabled\": \"true\",             \"restAuthCredential.username\": \"admin4468\",             \"restAuthCredential.password\": \"Password1!9688\"         }     } ";
            var properties = new HDInsightClusterCreateOrUpdateProperties()
            {
                ClusterVersion = "3.6",
                OSType = HDInsightOSType.Linux,
                Tier = HDInsightTier.Standard,
                ClusterDefinition = new HDInsightClusterDefinition()
                {
                    Kind = "Hadoop",
                    Configurations = new BinaryData(clusterDeifnitionConfigurations),
                },
                IsEncryptionInTransitEnabled = true,
            };
            properties.ComputeRoles.Add(new HDInsightClusterRole()
            {
                Name = "headnode",
                TargetInstanceCount = 2,
                HardwareVmSize = "Large",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = common_user,
                    Password = common_passwork
                }
            });
            properties.ComputeRoles.Add(new HDInsightClusterRole()
            {
                Name = "workernode",
                TargetInstanceCount = 3,
                HardwareVmSize = "Large",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = common_user,
                    Password = common_passwork
                }
            });
            properties.ComputeRoles.Add(new HDInsightClusterRole()
            {
                Name = "zookeepernode",
                TargetInstanceCount = 3,
                HardwareVmSize = "Small",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = common_user,
                    Password = common_passwork
                }
            });
            properties.StorageAccounts.Add(new HDInsightStorageAccountInfo()
            {
                Name = $"{storageAccountName}.blob.core.windows.net",
                IsDefault = true,
                Container = containerName,
                Key = accessKey,
            });
            return properties;
        }

        protected async Task<HDInsightClusterCreateOrUpdateProperties> PrepareClusterCreateParams(StorageAccountResource storageAccount)
        {
            string common_user = "sshuser5951";
            string common_passwork = "Password!5951";
            string containerName = GetRandomNumber("container");
            string accessKey = (await _storageAccount.GetKeysAsync()).Value.Keys.FirstOrDefault().Value;
            await storageAccount.GetBlobService().GetBlobContainers().CreateOrUpdateAsync(WaitUntil.Completed, containerName, new BlobContainerData());
            string clusterDeifnitionConfigurations = "{         \"gateway\": {             \"restAuthCredential.isEnabled\": \"true\",             \"restAuthCredential.username\": \"admin4468\",             \"restAuthCredential.password\": \"Password1!9688\"         }     } ";
            var properties = new HDInsightClusterCreateOrUpdateProperties()
            {
                ClusterVersion = "3.6",
                OSType = HDInsightOSType.Linux,
                Tier = HDInsightTier.Standard,
                ClusterDefinition = new HDInsightClusterDefinition()
                {
                    Kind = "Hadoop",
                    Configurations = new BinaryData(clusterDeifnitionConfigurations),
                },
                IsEncryptionInTransitEnabled = true,
            };
            properties.ComputeRoles.Add(new HDInsightClusterRole()
            {
                Name = "headnode",
                TargetInstanceCount = 2,
                HardwareVmSize = "Large",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = common_user,
                    Password = common_passwork
                }
            });
            properties.ComputeRoles.Add(new HDInsightClusterRole()
            {
                Name = "workernode",
                TargetInstanceCount = 3,
                HardwareVmSize = "Large",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = common_user,
                    Password = common_passwork
                }
            });
            properties.ComputeRoles.Add(new HDInsightClusterRole()
            {
                Name = "zookeepernode",
                TargetInstanceCount = 3,
                HardwareVmSize = "Small",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = common_user,
                    Password = common_passwork
                }
            });
            properties.StorageAccounts.Add(new HDInsightStorageAccountInfo()
            {
                Name = $"{storageAccount.Data.Name}.blob.core.windows.net",
                IsDefault = true,
                Container = containerName,
                Key = accessKey,
            });
            return properties;
        }
    }
}
