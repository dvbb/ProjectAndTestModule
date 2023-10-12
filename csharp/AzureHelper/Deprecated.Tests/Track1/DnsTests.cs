using Azure.Core;
using Azure.Identity;
using AzureHelper;
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
    internal class DnsTests
    {
        //[Test]
        //public async Task DnsTest()
        //{
        //    // craete a dns zone
        //    ServiceClientCredentials credentials = await GetDefaultCredentialAsync();
        //    DnsManagementClient dnsManagementClient = new DnsManagementClient(credentials);
        //    dnsManagementClient.SubscriptionId = subscription;
        //    Zone zone = new Zone()
        //    {
        //        Location = "global"
        //    };
        //    //var dnsZone = await dnsManagementClient.Zones.CreateOrUpdateAsync("Dns-RG-5107", "dns5951.com", zone);
        //    var list = (await dnsManagementClient.Zones.ListAsync()).ToList();
        //    Console.WriteLine(list.Count);
        //}
    }
}
