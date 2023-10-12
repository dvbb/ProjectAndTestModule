//using AzureSample;
//using System;
//using NUnit.Framework;
//using Microsoft.IdentityModel.Clients.ActiveDirectory;
//using Microsoft.Rest;
//using System.Net.Http;
//using Microsoft.Azure.Management.IotHub;
//using Azure.Core;
//using Azure.Identity;
//using System.Threading.Tasks;
//using System.Linq;

//namespace Track1
//{
//    public class IotHubTests : TestBase
//    {
//        [SetUp]
//        public void Setup()
//        {
//        }

//        /// <summary>
//        /// Prerequisites:
//        ///     Have a resource group named [IOT-RG-0000] and an IotHub named [iot-0000] in the corresponding subscription
//        /// </summary>
//        [Test]
//        public async Task IotHubTest()
//        {
//            string resourceGroupName = "IOT-RG-0000";
//            string iotHubName = "iot-0000";

//            // crud iot hub
//            ServiceClientCredentials credentials = await GetDefaultCredentialAsync();
//            IotHubClient iotHubClient = new IotHubClient(credentials);
//            iotHubClient.SubscriptionId = subscription;
            
//            //var iothub = iotHubClient.IotHubResource.Get(resourceGroupName, iotHubName);
//            //Console.WriteLine(iothub.Name);
//            var list = (await iotHubClient.IotHubResource.ListByResourceGroupAsync(resourceGroupName)).ToList();
//            Console.WriteLine(list.Count);
//        }
//    }
//}