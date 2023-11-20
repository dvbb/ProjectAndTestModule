using Azure.Core;
using Azure.ResourceManager.Resources;
using Azure;
using AzureHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.ResourceManager.AppService;
using Azure.ResourceManager;
using Azure.ResourceManager.AppService.Models;

namespace BasicServices.Tests
{
    internal class AppServiceTests
    {
        private ResourceGroupResource resourceGroup { get; set; }
        private string _rgName;

        [SetUp]
        public async Task SetUp()
        {
            _rgName = BaseClientExtension.CreateRandomName("AppServiceRG");
            BaseClient baseClient = new BaseClient();
            resourceGroup = await baseClient.CreateResourceGroup(_rgName);
            //resourceGroup = await baseClient.GetResourceGroup("AppServiceRG7393");

        }

        [TearDown]
        public async Task TearDown()
        {
            //resourceGroup.Delete(WaitUntil.Started);
        }

        [Test]
        public async Task DefaultAppService()
        {
            string appNamePrefix = "sampletestwebapp";
            var appName = BaseClientExtension.CreateRandomName(appNamePrefix);

            // Default app service plan is [F1] [Free tier]
            WebSiteCollection collection = resourceGroup.GetWebSites();
            WebSiteData data = new WebSiteData(resourceGroup.Data.Location) { };
            ArmOperation<WebSiteResource> lro = await collection.CreateOrUpdateAsync(WaitUntil.Completed, appName, data);
            WebSiteResource app = lro.Value;
        }

        [Test]
        public async Task AppService_With_SpecificPlan()
        {
            var planName = BaseClientExtension.CreateRandomName("plan");
            var appName = BaseClientExtension.CreateRandomName("sampletestwebapp");

            // Create a premium app service plan
            var planInput = new AppServicePlanData(resourceGroup.Data.Location)
            {
                Sku = new AppServiceSkuDescription()
                {
                    Name = "P1v3",
                    Tier = "PremiumV3",
                    Size = "P1v3",
                    Family = "Pv3",
                    Capacity = 1
                },
                Kind = "linux",
            };
            var appServicePlanLro = await resourceGroup.GetAppServicePlans().CreateOrUpdateAsync(WaitUntil.Completed, planName, planInput);
            AppServicePlanResource plan = appServicePlanLro.Value;

            // Create app service
            WebSiteData data = new WebSiteData(resourceGroup.Data.Location)
            {
                AppServicePlanId = plan.Data.Id
            };
            ArmOperation<WebSiteResource> lro = await resourceGroup.GetWebSites().CreateOrUpdateAsync(WaitUntil.Completed, appName, data);
            WebSiteResource app = lro.Value;

            // temp
            var slotData = new SlotConfigNamesResourceData
            {
                AppSettingNames = { "Example" },
            };
            var slot = await app.GetSlotConfigNamesResource().CreateOrUpdateAsync(WaitUntil.Completed, slotData);
        }
    }
}
