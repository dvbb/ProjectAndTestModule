using AzureSample;
using System;
using NUnit.Framework;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System.Net.Http;
using Microsoft.Azure.Management.IotHub;

namespace Track1
{
    public class Tests : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string resourceGroupName = "IOT-RG-0000";
            string iotHubName = "iot-0000";

            // get token
            ClientCredential cc = new ClientCredential(clientId, clientSecret);
            var context = new AuthenticationContext("https://login.microsoftonline.com/" + tenantId);
            var result = context.AcquireTokenAsync("https://management.azure.com/", cc);
            string AccessToken = result.Result.AccessToken;

            var bauthCredentials = new TokenCredentials(AccessToken);

            ServiceClientCredentials credentials = bauthCredentials;
            DelegatingHandler[] handlers = new DelegatingHandler[] { };

            // crud iot hub
            IotHubClient iotHubClient = new IotHubClient(credentials, handlers);
            iotHubClient.SubscriptionId = subscription;

            var iothub = iotHubClient.IotHubResource.Get(resourceGroupName, iotHubName);
            Console.WriteLine(iothub.Name);
        }
    }
}