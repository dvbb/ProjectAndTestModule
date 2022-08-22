//using System;
//using System.Threading.Tasks;
//using Azure;
//using Azure.Core;
//using Azure.Identity;
//using Azure.ResourceManager;
//using Azure.ResourceManager.Resources;
//using Azure.ResourceManager.Storage.Models;
//using AzureSample;
//using Azure.ResourceManager.Media;
//using Azure.ResourceManager.Media.Models;
//using Azure.ResourceManager.Storage;
//using NUnit.Framework;
//using Azure.ResourceManager.Network;

//namespace Track2
//{
//    internal class MediaServiceTests : TestBase
//    {
//        private ResourceGroupResource _resourceGroup;
//        private MediaServiceResource _mediaService;

//        [OneTimeSetUp]
//        public async Task GlobalSetUp()
//        {
//            // Create ArmClient
//            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
//            ArmClient armClient = new ArmClient(clientSecretCredential, subscription);

//            // Create a resource group
//            string rgName = "MediaService-Custom-RG-0000";
//            ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
//            ResourceGroupData rgData = new ResourceGroupData(AzureLocation.WestUS2) { };
//            var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
//            _resourceGroup = rgLro.Value;

//            // Create a Storage account
//            string storageAccountName = "azstorage22081053";
//            StorageAccountCreateOrUpdateContent storagedata = new StorageAccountCreateOrUpdateContent(new StorageSku(StorageSkuName.StandardLrs), StorageKind.BlobStorage, _resourceGroup.Data.Location)
//            {
//                AccessTier = StorageAccountAccessTier.Hot,
//            };
//            var storage = await _resourceGroup.GetStorageAccounts().CreateOrUpdateAsync(WaitUntil.Completed, storageAccountName, storagedata);

//            // Create network
//            var vnetName = "vnet-0000";
//            VirtualNetworkData networkdata = new VirtualNetworkData()
//            {
//                Location = _resourceGroup.Data.Location,
//            };
//            networkdata.AddressPrefixes.Add("10.10.0.0/16");
//            networkdata.Subnets.Add(new SubnetData() { Name = "subnet1", AddressPrefix = "10.10.1.0/24" });
//            networkdata.Subnets.Add(new SubnetData() { Name = "subnet2", AddressPrefix = "10.10.2.0/24" });
//            var vnet = await _resourceGroup.GetVirtualNetworks().CreateOrUpdateAsync(WaitUntil.Completed, vnetName, networkdata);

//            // Create a media service
//            string mediaServiceName = "mediacustom0000";
//            MediaServiceData data = new MediaServiceData(_resourceGroup.Data.Location);
//            data.StorageAccounts.Add(new MediaServiceStorageAccount(MediaServiceStorageAccountType.Primary) { Id = storage.Value.Data.Id });
//            var mediaServiceLro = await _resourceGroup.GetMediaServices().CreateOrUpdateAsync(WaitUntil.Completed, mediaServiceName, data);
//            _mediaService = mediaServiceLro.Value;
//        }

//        [Test]
//        public async Task LiveEvent_GetAll()
//        {
//            var collection = _mediaService.GetLiveEvents();

//            var existsLiveEvent = await collection.GetAsync("liveevent0000");
//            Console.WriteLine(existsLiveEvent.Value.Data.Name);

//            LiveEventData data = new LiveEventData(_mediaService.Data.Location)
//            {
//                Input = new LiveEventInput(LiveEventInputProtocol.Rtmp)
//                {
//                },
//                CrossSiteAccessPolicies = new CrossSiteAccessPolicies(),
//                Description = "test",
//            };
//            string liveEventName = "liveEvent003";
//            var liveEvent = await collection.CreateOrUpdateAsync(WaitUntil.Completed, liveEventName, data);
//            Assert.IsNotNull(liveEvent);
//            Console.WriteLine(liveEvent.Value.Data.Name);

//            //await foreach (var item in collection.GetAllAsync())
//            //{
//            //    Console.WriteLine(item.Data.Id);
//            //}
//        }

//        [Test]
//        public async Task LiveEventOutPut_GetAll()
//        {
//            var existsLiveEvent = await _mediaService.GetLiveEvents().GetAsync("liveevent0000");
//            Console.WriteLine(existsLiveEvent.Value.Data.Name);

//            var collection = existsLiveEvent.Value.GetLiveOutputs();

//            var asset = await _mediaService.GetMediaAssets().CreateOrUpdateAsync(WaitUntil.Completed, "empty-asset-input", new MediaAssetData());
//            LiveOutputData data = new LiveOutputData()
//            {
//                AssetName = asset.Value.Data.Name,
//                ArchiveWindowLength = new TimeSpan(0,5,0),
//                HttpLiveStreaming = new Hls(),
//            };
//            var output = await collection.CreateOrUpdateAsync(WaitUntil.Completed,"output1253",data);

//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }
//        }

//        [Test]
//        public async Task PrivateEndpoint_GetAll()
//        {
//            var collection = _mediaService.GetMediaPrivateEndpointConnections();

//            //string connName = "connection15823241";
//            //MediaPrivateEndpointConnectionData data = new MediaPrivateEndpointConnectionData()
//            //{
//            //};
//            //var connection = await collection.CreateOrUpdateAsync(WaitUntil.Completed, connName, data);

//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }
//        }

//        [Test]
//        public async Task PrivateLink_GetAll()
//        {
//            var collection = _mediaService.GetMediaPrivateLinkResources();

//            //string connName = "connection15823241";
//            //MediaPrivateEndpointConnectionData data = new MediaPrivateEndpointConnectionData()
//            //{
//            //};
//            //var connection = await collection.CreateOrUpdateAsync(WaitUntil.Completed, connName, data);

//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }
//        }

//        [Test]
//        public async Task Transform_E2E()
//        {
//            var collection = _mediaService.GetMediaTransforms();

//            string mediaTransformName = "randomtransfer7423";
//            MediaTransformData data = new MediaTransformData()
//            {
//            };
//            MediaPreset preset = new AudioAnalyzerPreset();
//            MediaTransformOutput para = new MediaTransformOutput(preset);
//            data.Outputs.Add(para);
//            var mediaTransform = await collection.CreateOrUpdateAsync(WaitUntil.Completed, mediaTransformName, data);

//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }
//        }

//        [Test]
//        public async Task TransformJob_E2E()
//        {
//            var collection = _mediaService.GetMediaTransforms();

//            string mediaTransformName = "randomtransfer7423";
//            MediaTransformData data = new MediaTransformData();
//            data.Outputs.Add(new MediaTransformOutput(new AudioAnalyzerPreset()));
//            var mediaTransform = await collection.CreateOrUpdateAsync(WaitUntil.Completed, mediaTransformName, data);

//            // create two asset
//            var mediaAsset1 = await _mediaService.GetMediaAssets().CreateOrUpdateAsync(WaitUntil.Completed, "empty-asset-input", new MediaAssetData());
//            var mediaAsset2 = await _mediaService.GetMediaAssets().CreateOrUpdateAsync(WaitUntil.Completed, "empty-asset-output", new MediaAssetData());

//            var jobCollection = mediaTransform.Value.GetMediaTransformJobs();

//            MediaTransformJobData jobdata = new MediaTransformJobData();
//            jobdata.Input = new MediaTransformJobInputAsset("empty-asset-input");
//            jobdata.Outputs.Add(new MediaTransformJobOutputAsset("empty-asset-output"));
//            var job = await jobCollection.CreateOrUpdateAsync(WaitUntil.Completed, "customjob1", jobdata);

//            await foreach (var item in jobCollection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }
//        }


//        [Test]
//        public async Task StreamingEndpoint_E2E()
//        {
//            var collection = _mediaService.GetStreamingEndpoints();

//            StreamingEndpointData data = new StreamingEndpointData(_resourceGroup.Data.Location);
//            var streamingEndpoint = await collection.CreateOrUpdateAsync(WaitUntil.Completed, "streamEndpoint00584", data);

//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }
//        }

//        [Test]
//        public async Task StreamingLocator_E2E()
//        {
//            var collection = _mediaService.GetStreamingLocators();

//            var mediaAsset1 = await _mediaService.GetMediaAssets().CreateOrUpdateAsync(WaitUntil.Completed, "empty-asset-for-locator-1542", new MediaAssetData());

//            StreamingLocatorData data = new StreamingLocatorData()
//            {
//                AssetName = mediaAsset1.Value.Data.Name,
//                StreamingPolicyName = "Predefined_ClearStreamingOnly"
//            };
//            var locator = await collection.CreateOrUpdateAsync(WaitUntil.Completed, "streamingLocator2154", data);

//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }
//        }

//        [Test]
//        public async Task StreamingPolicy_E2E()
//        {
//            var collection = _mediaService.GetStreamingPolicies();

//            StreamingPolicyData data = new StreamingPolicyData()
//            {
//                EnvelopeEncryption = new EnvelopeEncryption()
//                {
//                    EnabledProtocols = new MediaEnabledProtocols(false, true, true, true)
//                },
//            };
//            var policy = await collection.CreateOrUpdateAsync(WaitUntil.Completed, "streamingPolicy152224", data);

//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }

//            await policy.Value.DeleteAsync(WaitUntil.Completed);

//            Console.WriteLine("\n\n");

//            await foreach (var item in collection.GetAllAsync())
//            {
//                Console.WriteLine(item.Data.Id);
//            }
//        }
//    }
//}
