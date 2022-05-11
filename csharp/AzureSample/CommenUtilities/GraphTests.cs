using Azure.Identity;
using AzureSample;
using Microsoft.Graph;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommenUtilities
{
    public class GraphTests : TestBase
    {
        [SetUp]
        public void Setup()
        {
            // use Microsoft.Graph need the Application have corresponding api permission
            //  step:
            //     azure portal => search [App registrations] => click your want to get the application
            //     => Api permission => click [Add a permission] button => ... => click [Grant admin consent for default derectory] button  
        }

        [Test]
        public async Task GetServicePrincipalInfo()
        {
            // https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential

            string customTenantId = "";
            string customClientId = "";
            string customClientSecret = "";

            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };
            var clientSecretCredential = new ClientSecretCredential(customTenantId, customClientId, customClientSecret, options);
            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            var response = await graphClient.ServicePrincipals.Request().GetAsync();
            var result = response.CurrentPage.Where(i => i.DisplayName == "PublicAppRegistrationTest").FirstOrDefault();
            Console.WriteLine(result.DisplayName);
            Console.WriteLine(result.Id);
            Console.WriteLine(result.AppId);
        }

        [Test]
        public async Task GetCustomUserInfo()
        {
            string customTenantId = "";
            string customClientId = "";
            string customClientSecret = "";

            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };
            var clientSecretCredential = new ClientSecretCredential(customTenantId, customClientId, customClientSecret, options);
            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            var response = await graphClient.Users.Request().GetAsync();
            var list = response.CurrentPage;
            Console.WriteLine(list[0].UserPrincipalName);
            Console.WriteLine(list[0].Surname);
        }
    }
}