using Azure.Core;
using Azure.Identity;
using AzureSample;
using Microsoft.Azure.Management.ContainerInstance;
using Microsoft.Rest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track1
{
    internal class ContainerInstanceTests : TestBase
    {
        [Test]
        public async Task ContainerInstanceTestAsync()
        {
            // Get AccessToken with Azure.Identity
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            string[] scopes = { "https://management.core.windows.net/.default" };
            TokenRequestContext tokenRequestContext = new TokenRequestContext(scopes, "");
            var response = await clientSecretCredential.GetTokenAsync(tokenRequestContext);
            string accessToken = response.Token;

            // Get a existing an ADF pipeline
            TokenCredentials bauthCredentials = new TokenCredentials(accessToken);
            ServiceClientCredentials credentials = bauthCredentials;

            string rgName = "graph-rg-0000";
            ContainerInstanceManagementClient client = new ContainerInstanceManagementClient(credentials);
            client.SubscriptionId = subscription;
            //var data  = await client.Containers.ListLogsAsync();
            var list1 =(await client.Location.ListCapabilitiesAsync("ukwest")).ToList();
            var list2 = (await client.Location.ListCapabilitiesAsync("eastus")).ToList();
            foreach (var item in list1)
            {
                Console.WriteLine($"{item.Gpu}\t{item.IpAddressType}\t{item.Location}\t{item.OsType}\t{item.ResourceType}");
            }
            Console.WriteLine();
            foreach (var item in list2)
            {
                Console.WriteLine($"{item.Gpu}\t{item.IpAddressType}\t{item.Location}\t{item.OsType}\t{item.ResourceType}");
            }
        }
    }
}
