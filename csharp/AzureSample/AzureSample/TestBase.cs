using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace AzureSample
{
    public class TestBase
    {
        public string clientId => Environment.GetEnvironmentVariable("CLIENT_ID");
        public string clientSecret => Environment.GetEnvironmentVariable("CLIENT_SECRET");
        public string tenantId => Environment.GetEnvironmentVariable("TENANT_ID");
        public string subscription => Environment.GetEnvironmentVariable("SUBSCRIPTION_ID");

        public ArmClient Client { get; }
        public SubscriptionResource DefaultSubscription { get; }

        public TestBase()
        {
            CheckEffective(clientId);
            CheckEffective(clientSecret);
            CheckEffective(tenantId);
            CheckEffective(subscription);

            Client = new ArmClient(new ClientSecretCredential(tenantId, clientId, clientSecret), subscription);
            DefaultSubscription = Client.GetDefaultSubscriptionAsync().Result;
        }

        private void CheckEffective(string value)
        {
            if (value == null || string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException($"argument cannot be null.please make sure all enviroment parameters are correct.");
            }
        }

        public async Task<string> GetToken()
        {
            // Get AccessToken with Azure.Identity
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            string[] scopes = { "https://management.core.windows.net/.default" };
            TokenRequestContext tokenRequestContext = new TokenRequestContext(scopes, "");
            var response = await clientSecretCredential.GetTokenAsync(tokenRequestContext);
            return response.Token;
        }

        public async Task<ServiceClientCredentials> GetDefaultCredentialAsync()
        {
            string accessToken = await this.GetToken();

            // Get a existing an ADF pipeline
            TokenCredentials bauthCredentials = new TokenCredentials(accessToken);
            return bauthCredentials;
        }

        public string GetRandomNumber(string resource)
        {
            Random random = new Random();
            return $"{resource}-{random.Next(9999)}";
        }
    }
}
