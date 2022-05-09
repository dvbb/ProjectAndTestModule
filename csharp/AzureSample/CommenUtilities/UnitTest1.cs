using Azure.Identity;
using AzureSample;
using Microsoft.Graph;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommenUtilities
{
    public class Tests : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetUserInfo()
        {
            // https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);
            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            var data = await graphClient.ServicePrincipals.Request().GetAsync();
        }
    }
}