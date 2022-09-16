using Azure;
using Azure.Core;
using Azure.ResourceManager.Peering;
using Azure.ResourceManager.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Track2.Helper;

namespace Track2
{
    internal class PeeringTests : Track2TestBase
    {
        private ResourceGroupResource _resourceGroup;

        [SetUp]
        public async Task TestSetUp()
        {
            string rgName = "PeeringRG0000";
            _resourceGroup = await CreateResourceGroup(rgName, AzureLocation.EastUS);
        }

        [Test]
        public async Task PeeringService()
        {
            var peeringCollection = _resourceGroup.GetPeeringServices();

            // Create
            string peeringName = GetRandomNumber("testpeering");
            PeeringServiceData data = new PeeringServiceData(_resourceGroup.Data.Location)
            {
                Location = _resourceGroup.Data.Location,
                PeeringServiceLocation = "South Australia",
                PeeringServiceProvider = "Atman",
                ProviderPrimaryPeeringLocation = "Warsaw",
            };
            //var peering = await peeringCollection.CreateOrUpdateAsync(WaitUntil.Completed, peeringName, data);

            // Getall
            await foreach (var item in peeringCollection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }
        }
    }
}
