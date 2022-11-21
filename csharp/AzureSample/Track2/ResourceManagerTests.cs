using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using NUnit.Framework;
using Track2.Helper;

namespace Track2
{
    public class ResourceManagerTests : Track2TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SubscriptionTagTest()
        {
            var subscriptionCollection = Client.GetSubscriptions();
            var tagToLookFor = "TagKey-9823";

            var iterationCount = 0;

            int i = 10;

            while (i-->0)
            {
                Console.WriteLine($"Iteration #{iterationCount}");

                var subscriptionResources = subscriptionCollection.GetAllAsync();  // stale

                await foreach (var subscriptionResource in subscriptionResources)
                {
                    if (subscriptionResource.Data.Tags.ContainsKey(tagToLookFor))
                    {
                        Console.WriteLine($">> {subscriptionResource.Data.DisplayName} ({tagToLookFor} tag): {subscriptionResource.Data.Tags[tagToLookFor]}");
                    }
                }

                Console.WriteLine();

                iterationCount++;
                await Task.Delay(10000);
            }
        }
    }
}