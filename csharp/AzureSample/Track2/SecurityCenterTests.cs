using Azure;
using Azure.Core;
using Azure.ResourceManager.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azure.ResourceManager.SecurityCenter;
using Track2.Helper;

namespace Track2
{
    internal class SecurityCenterTests : Track2TestBase
    {
        private ResourceGroupResource _resourceGroup;

        [SetUp]
        public async Task TestSetUp()
        {
            _resourceGroup = await CreateResourceGroup("SecurityCenterRG-0000", AzureLocation.EastUS);
            var vm = await CreateDefaultVirtualMachine(_resourceGroup, "vm0000");
        }

        [Test]
        public async Task AdaptiveNetworkHardeningE2E()
        {
            var Collection = _resourceGroup.GetAdaptiveNetworkHardenings(_resourceGroup.Data.Name, "Microsoft.Compute", "vm0000");
        }
    }
}
