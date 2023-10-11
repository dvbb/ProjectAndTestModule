using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;

namespace AzureHelper
{
    public class BaseClient
    {
        private const string dummySSHKey = "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQC+wWK73dCr+jgQOAxNsHAnNNNMEMWOHYEccp6wJm2gotpr9katuF/ZAdou5AaW1C61slRkHRkpRRX9FA9CYBiitZgvCCz+3nWNN7l/Up54Zps/pHWGZLHNJZRYyAB6j5yVLMVHIHriY49d/GZTZVNB8GoJv9Gakwc/fuEZYYl4YDFiGMBP///TzlI4jhiJzjKnEvqPFki5p2ZRJqcbCiF4pJrxUQR/RXqVFQdbRLZgYfJ8xGB878RENq3yQ39d8dVOkq4edbkzwcUmwwwkYVPIoDGsYLaRHnG+To7FvMeyO7xDVQkMKzopTQV8AuKpyvpqu0a9pWOMaiCyDytO7GGN you@me.com";
        public string? clientId => Environment.GetEnvironmentVariable("CLIENT_ID");
        public string? clientSecret => Environment.GetEnvironmentVariable("CLIENT_SECRET");
        public string? tenantId => Environment.GetEnvironmentVariable("TENANT_ID");
        public string? subscription => Environment.GetEnvironmentVariable("SUBSCRIPTION_ID");
        
        public AzureLocation DefaultLocation = AzureLocation.EastUS;

        public ArmClient Client { get; }
        public SubscriptionResource DefaultSubscription { get; }
        public TenantResource DefaultTenant => GetTenant().Result;

        public BaseClient()
        {
            CheckEffective(clientId);
            CheckEffective(clientSecret);
            CheckEffective(tenantId);
            CheckEffective(subscription);

            Client = new ArmClient(new ClientSecretCredential(tenantId, clientId, clientSecret), subscription);
            DefaultSubscription = Client.GetDefaultSubscriptionAsync().Result;
        }

        private async Task<TenantResource> GetTenant()
        {
            var tenants = await Client.GetTenants().GetAllAsync().ToEnumerableAsync();
            return tenants.FirstOrDefault();
        }

        private void CheckEffective(string? value)
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

        public string GetRandomNumber(string resource)
        {
            Random random = new Random();
            return $"{resource}{random.Next(9999)}";
        }

        public async Task<ResourceGroupResource> CreateResourceGroup(string resourceGroupName)
        {
            var rgLro = await DefaultSubscription.GetResourceGroups().CreateOrUpdateAsync(Azure.WaitUntil.Completed, resourceGroupName, new ResourceGroupData(DefaultLocation));
            return rgLro.Value;
        }

        public async Task<ResourceGroupResource> CreateResourceGroup(string resourceGroupName, AzureLocation location)
        {
            var rgLro = await DefaultSubscription.GetResourceGroups().CreateOrUpdateAsync(Azure.WaitUntil.Completed, resourceGroupName, new ResourceGroupData(location));
            return rgLro.Value;
        }

        public async Task<ResourceGroupResource> GetResourceGroup(string resourceGroupName)
        {
            var rgLro = await DefaultSubscription.GetResourceGroups().GetAsync(resourceGroupName);
            return rgLro.Value;
        }


    }
}