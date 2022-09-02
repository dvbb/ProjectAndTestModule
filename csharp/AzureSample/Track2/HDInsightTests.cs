using Azure.Core;
using Azure.ResourceManager.HDInsight;
using Azure.ResourceManager.HDInsight.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track2.Helper;

namespace Track2
{
    internal class HDInsightTests : Track2TestBase
    {
        private ResourceGroupResource _resourceGroup;
        private StorageAccountResource _storageAccount;

        [OneTimeSetUp]
        public async Task GlobalSetUp()
        {
            string resourceGroupName = "HDInsightRG-0000";
            string storageAccountName = "storageacoount220902";
            _resourceGroup = await CreateResourceGroup(resourceGroupName, AzureLocation.EastUS);
            //_storageAccount = await CreateDefaultStorage(_resourceGroup, storageAccountName);
            _storageAccount = await _resourceGroup.GetStorageAccounts().GetAsync("cluster0000hdistorage ");
        }

        [Test]
        public async Task Cluster_CreateHadoop()
        {
            var collection = _resourceGroup.GetHDInsightClusters();

            var key = await _storageAccount.GetKeysAsync();
            string clusterName = "cluster1565";
            var storageAccountInfo = new HDInsightStorageAccountInfo()
            {
                Name = _storageAccount.Data.Name,
                ResourceId = _storageAccount.Data.Id,
                Container = $"{_storageAccount.Data.Name}-2022-09-02t02-09-29-551z",
                IsDefault = true,
                Key = key.Value.Keys[0].Value.ToString(),
            };

            var Properties = new HDInsightClusterCreateOrUpdateProperties()
            {
                Tier = HDInsightTier.Standard,
                DiskEncryptionProperties = new HDInsightDiskEncryptionProperties() { },
                IsEncryptionInTransitEnabled = false,
                MinSupportedTlsVersion = "1.2",
                ClusterVersion = "4.0.3000.1",
                OSType = HDInsightOSType.Linux,
                ComputeIsolationProperties = new HDInsightComputeIsolationProperties()
                {
                    EnableComputeIsolation = false
                },
                ClusterDefinition = new HDInsightClusterDefinition()
                {
                    Kind = "HADOOP",
                },
            };
            Properties.ClusterDefinition.ComponentVersion.Add(new KeyValuePair<string, string>("Hadoop", "3.1"));
            Properties.StorageAccounts.Add(storageAccountInfo);

            var role_headnode = new HDInsightClusterRole()
            {
                Name = "headnode",
                TargetInstanceCount = 2,
                HardwareVmSize = "standard_e4_v3",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = "sshuser",
                    Password = "Xx123456123*"
                },
            };
            var role_workernode = new HDInsightClusterRole()
            {
                Name = "workernode",
                TargetInstanceCount = 4,
                HardwareVmSize = "standard_e8_v3",
                OSLinuxProfile = new HDInsightLinuxOSProfile()
                {
                    Username = "sshuser",
                    Password = "Xx123456123*"
                },
            };
            Properties.ComputeRoles.Add(role_headnode);
            Properties.ComputeRoles.Add(role_workernode);

            var data = new HDInsightClusterCreateOrUpdateContent()
            {
                Properties = Properties,
                Location = _resourceGroup.Data.Location,
            };
            var cluster = await collection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, clusterName, data);

            Console.WriteLine(cluster.Value.Data.Name);
        }

        [Test]
        //[Ignore("Unhandled exception occurred in api while processing your request. Please try again.")]
        public async Task Cluster_LinuxHadoopSshPassword()
        {
            var collection = _resourceGroup.GetHDInsightClusters();
            string clusterName = "hadoopcluster0000222";
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
                Name = $"cluster0000hdistorage.blob.core.windows.net",
                IsDefault = true,
                Container = "container8000",
                Key = key,
            });


            var data = new HDInsightClusterCreateOrUpdateContent()
            {
                Properties = properties,
                Location = _resourceGroup.Data.Location,
            };
            data.Tags.Add(new KeyValuePair<string,string>("key0","value0"));
            var cluster = await collection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, clusterName, data);

            Console.WriteLine(cluster.Value.Data.Name);
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
    }
}
