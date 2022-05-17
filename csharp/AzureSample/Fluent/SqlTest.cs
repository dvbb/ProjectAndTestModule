using AzureSample;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.Sql.Fluent;
using Microsoft.Azure.Management.Sql.Fluent.Models;
using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Fluent
{
    public class Tests : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Prerequisites:
        ///     Have a resource group named [Sql-RG-0000] and a Server named [server-0000] in the corresponding subscription
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Get_Server()
        {
            //string resourceGroupName = "Sql-RG-0000";
            //string serverName = "server-0000";

            //var creds = new AzureCredentialsFactory().FromServicePrincipal(clientId, clientSecret, tenantId, AzureEnvironment.AzureGlobalCloud);
            //var azure = Azure.Authenticate(creds).WithSubscription(subscription);
            //var server = azure.SqlServers.GetByResourceGroup(resourceGroupName, serverName);
            //Console.WriteLine(server.Name);
            //Console.WriteLine(server.RegionName);

            //var sqlManager = SqlManager.Authenticate(creds, subscription);
            //SqlManagementClient client = new SqlManagementClient(sqlManager.RestClient);
            //client.SubscriptionId = subscription;
            //var policy = await client.ExtendedServerBlobAuditingPolicies.GetWithHttpMessagesAsync("Sql-RG-0000", "server-0000");
            //Console.WriteLine("server-0000: " + policy.Body.State);
        }
    }
}