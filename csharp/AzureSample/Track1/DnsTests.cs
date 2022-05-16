using Azure.Core;
using Azure.Identity;
using AzureSample;
using Microsoft.Azure.Management.Dns;
using Microsoft.Azure.Management.Dns.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Track1
{
    internal class DnsTests : TestBase
    {
        [Test]
        public async Task DnsTest()
        {
            // Get AccessToken with Azure.Identity
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            string[] scopes = { "https://management.core.windows.net/.default" };
            TokenRequestContext tokenRequestContext = new TokenRequestContext(scopes, "");
            var response = await clientSecretCredential.GetTokenAsync(tokenRequestContext);
            string accessToken = response.Token;
            TokenCredentials bauthCredentials = new TokenCredentials(accessToken);
            ServiceClientCredentials credentials = bauthCredentials;

            // craete a dns zone
            DnsManagementClient dnsManagementClient = new DnsManagementClient(credentials);
            dnsManagementClient.SubscriptionId = subscription;
            Zone zone = new Zone()
            {
                Location = "global"
            };
            //var dnsZone = await dnsManagementClient.Zones.CreateOrUpdateAsync("Dns-RG-5107", "dns5951.com", zone);
            var list = (await dnsManagementClient.Zones.ListAsync()).ToList();
            Console.WriteLine(list.Count);
        }

    }
}
