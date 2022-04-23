using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Track1
{
    internal class AdfAzureIdentity
    {
        [Test]
        public async Task GetAdfPipelineAzureIdentity()
        {
            // Get credential value with Azure.Identity
            var secretClient = new SecretClient(new Uri("https://AdfKeyVault220421.vault.azure.net"), new DefaultAzureCredential());
            var clientIdSecret = secretClient.GetSecret("ClientId").Value;
            var clientSecretSecret = secretClient.GetSecret("ClientSecret").Value;
            var tenantIdSecret = secretClient.GetSecret("tenantId").Value;
            var subscriptionSecret = secretClient.GetSecret("subscription").Value;

            string clientId = clientIdSecret.Value;
            string clientSecret = clientSecretSecret.Value;
            string tenantId = tenantIdSecret.Value;
            string subscription = subscriptionSecret.Value;

            Console.WriteLine(clientId);
            Console.WriteLine(clientSecret);
            Console.WriteLine(tenantId);
            Console.WriteLine(subscription);

            // get token
            ClientCredential cc = new ClientCredential(clientId, clientSecret);
            var context = new AuthenticationContext("https://login.microsoftonline.com/" + tenantId);
            var result = context.AcquireTokenAsync("https://management.azure.com/", cc);
            string AccessToken = result.Result.AccessToken;
            TokenCredentials bauthCredentials = new TokenCredentials(AccessToken);
            ServiceClientCredentials credentials = bauthCredentials;
            DelegatingHandler[] handlers = new DelegatingHandler[] { };

            // get a pipeline of data factory
            string resourceGroupName = "";
            string dataFactoryName = "";
            string pipelineName = "";
            var dataFactoryclient = new DataFactoryManagementClient(credentials)
            {
                SubscriptionId = subscription
            };
            var factory = await dataFactoryclient.Factories.GetAsync(resourceGroupName, dataFactoryName);

            var pipeline = await dataFactoryclient.Pipelines.GetAsync(resourceGroupName, dataFactoryName, pipelineName);
        }
    }
}
