using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.CognitiveServices;
using Microsoft.Azure.Management.CognitiveServices.Models;
using NUnit.Framework;
using AzureSample;

namespace Track1
{
    internal class CognitiveServicesManagementClientTests : TestBase
    {
        [Test]
        public async Task GetAuth()
        {
            const string service_principal_application_id = "PASTE_YOUR_SERVICE_PRINCIPAL_APPLICATION_ID_HERE";
            const string service_principal_secret = "PASTE_YOUR_SERVICE_PRINCIPAL_SECRET_HERE";
            var service_principal_credentials = new ServicePrincipalLoginInformation();
            service_principal_credentials.ClientId = service_principal_application_id;
            service_principal_credentials.ClientSecret = service_principal_secret;

            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(clientId, clientSecret, tenantId, AzureEnvironment.AzureGlobalCloud);
            CognitiveServicesManagementClient client = new CognitiveServicesManagementClient(credentials);
            client.SubscriptionId = subscription;
        }
    }
}
