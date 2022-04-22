using AzureSample;
using Microsoft.Azure.Management.Dns;
using Microsoft.Azure.Management.Dns.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            // get token
            ClientCredential cc = new ClientCredential(clientId, clientSecret);
            var context = new AuthenticationContext("https://login.microsoftonline.com/" + tenantId);
            var result = context.AcquireTokenAsync("https://management.azure.com/", cc);
            string AccessToken = result.Result.AccessToken;
            var bauthCredentials = new TokenCredentials(AccessToken);
            ServiceClientCredentials credentials = bauthCredentials;
            DelegatingHandler[] handlers = new DelegatingHandler[] { };

            // craete a dns zone
            DnsManagementClient dnsManagementClient = new DnsManagementClient(credentials, handlers);
            dnsManagementClient.SubscriptionId = subscription;
            Zone zone = new Zone()
            {
                Location = "global"
            };
            var dnsZone = await dnsManagementClient.Zones.CreateOrUpdateAsync("Dns-RG-5107", "dns5951.com", zone);

        }

    }
}
