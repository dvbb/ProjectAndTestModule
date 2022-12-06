using AzureSample;
using Microsoft.Azure.Management.Dns.Models;
using Microsoft.Azure.Management.Dns;
using Microsoft.Rest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Azure.Management.DataLake.Analytics;

namespace Track1
{
    internal class DataLakeAnalyticsTests : TestBase
    {
        [Test]
        public async Task DnsTest()
        {
            // craete a dns zone
            ServiceClientCredentials credentials = await GetDefaultCredentialAsync();
            //DataLakeAnalyticsJobManagementClient client = new DataLakeAnalyticsJobManagementClient(credentials);

            // get
            string accountName = "";
            Guid jobIdentity= Guid.NewGuid();
            //var response  = await client.Job.GetAsync(accountName, jobIdentity);
        }
    }
}
