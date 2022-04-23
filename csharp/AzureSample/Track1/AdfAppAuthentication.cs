using Azure.Identity;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Track1
{
    internal class AdfAppAuthentication
    {
        [Test]
        public async Task GetAdfPipelineAppAuthen()
        {
            // Get authentication value with AppAuthentication
            AzureServiceTokenProvider tokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));

            var clientIdSecretBundle = await keyVaultClient.GetSecretAsync("https://AdfKeyVault220421.vault.azure.net/secrets/ClientId");
            var clientSecretSecretBundle = await keyVaultClient.GetSecretAsync("https://AdfKeyVault220421.vault.azure.net/secrets/ClientSecret");
            var tenantIdSecretBundle = await keyVaultClient.GetSecretAsync("https://AdfKeyVault220421.vault.azure.net/secrets/tenantId");
            var subscriptionSecretBundle = await keyVaultClient.GetSecretAsync("https://AdfKeyVault220421.vault.azure.net/secrets/subscription");
            string clientId = clientIdSecretBundle.Value;
            string clientSecret = clientSecretSecretBundle.Value;
            string tenantId = tenantIdSecretBundle.Value;
            string subscription = subscriptionSecretBundle.Value;

            Console.WriteLine(clientId);
            Console.WriteLine(clientSecret);
            Console.WriteLine(tenantId);
            Console.WriteLine(subscription);

            Console.WriteLine("Get token...");
            Console.WriteLine("Get a pipeline of data factory...");

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
