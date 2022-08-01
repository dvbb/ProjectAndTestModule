using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Dns;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using AzureSample;
using NUnit.Framework;

namespace Track2
{
    internal class DnsTests : TestBase
    {
        private ResourceGroupResource _resourceGroup;
        private DnsZoneResource _dnsZone;

        [OneTimeSetUp]
        public async Task GlobalSetUp()
        {
            // Create ArmClient
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            ArmClient armClient = new ArmClient(clientSecretCredential, subscription);

            // Create a resource group
            string rgName = "Dns-Custom-RG-0000";
            ResourceGroupCollection rgCollection = armClient.GetDefaultSubscriptionAsync().Result.GetResourceGroups();
            ResourceGroupData rgData = new ResourceGroupData(AzureLocation.WestUS2) { };
            var rgLro = await rgCollection.CreateOrUpdateAsync(Azure.WaitUntil.Completed, rgName, rgData);
            _resourceGroup = rgLro.Value;

            //Create DnsZone
            string dnsZoneName = "220726sample0000.com";
            DnsZoneData data = new DnsZoneData("Global")
            {
            };
            var dnsLro = await _resourceGroup.GetDnsZones().CreateOrUpdateAsync(WaitUntil.Completed, dnsZoneName, data);
            _dnsZone = dnsLro.Value;
        }

        [Test]
        public async Task DnsZoneE2E()
        {
            //Create DnsZone
            string dnsZoneName = "220726sample9999.com";
            DnsZoneData data = new DnsZoneData("Global")
            {
            };
            var dnsLro = await _resourceGroup.GetDnsZones().CreateOrUpdateAsync(WaitUntil.Completed, dnsZoneName, data);
            var dnsZone = dnsLro.Value;
            var collection = _resourceGroup.GetDnsZones();

            // exist
            bool flag = await collection.ExistsAsync(dnsZoneName);
            Assert.IsTrue(flag);

            // get
            var get = await collection.GetAsync(dnsZoneName);
            Assert.IsNotNull(get);
            Assert.AreEqual(dnsZoneName, get.Value.Data.Name);

            // getall
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }

            // delete
            await dnsZone.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync(dnsZoneName);
            Assert.IsFalse(flag);
        }

        [Test]
        public async Task AaaaRecordE2E()
        {
            var collection = _dnsZone.GetRecordSetAaaas();
            string name = "aaaa";
            var recordSetAaaaResource = await collection.CreateOrUpdateAsync(WaitUntil.Completed, name, new AaaaRecordSetData() { });
            Assert.IsNotNull(recordSetAaaaResource);
            Assert.IsNotNull(recordSetAaaaResource.Value.Data.ETag);
            Assert.AreEqual(name, recordSetAaaaResource.Value.Data.Name);
            Assert.AreEqual("Succeeded", recordSetAaaaResource.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/AAAA", recordSetAaaaResource.Value.Data.ResourceType.Type);

            // exist
            bool flag = await collection.ExistsAsync("aaaa");
            Assert.IsTrue(flag);

            // get
            var getResponse = await collection.GetAsync("aaaa");
            Assert.IsNotNull(getResponse);
            Assert.AreEqual(name, getResponse.Value.Data.Name);
            Assert.IsNotNull(getResponse.Value.Data.TtlInSeconds);
            Console.WriteLine(getResponse.Value.Data.TtlInSeconds);

            // getall
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }

            // delete
            await recordSetAaaaResource.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync("aaaa");
            Assert.IsFalse(flag);
        }
   
        [Test]
        public async Task ARecordE2E()
        {
            var collection = _dnsZone.GetRecordSetACollections();
            string name = "a";
            var recordSetAResource = await collection.CreateOrUpdateAsync(WaitUntil.Completed, name, new ARecordSetData() { });
            Assert.IsNotNull(recordSetAResource);
            Assert.IsNotNull(recordSetAResource.Value.Data.ETag);
            Assert.AreEqual(name, recordSetAResource.Value.Data.Name);
            Assert.AreEqual("Succeeded", recordSetAResource.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/A", recordSetAResource.Value.Data.ResourceType.Type);

            // exist
            bool flag = await collection.ExistsAsync("a");
            Assert.IsTrue(flag);

            // get
            var getResponse = await collection.GetAsync(name);
            Assert.IsNotNull(getResponse);
            Assert.AreEqual(name, getResponse.Value.Data.Name);
            Assert.AreEqual("Succeeded", getResponse.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/A", getResponse.Value.Data.ResourceType.Type);
            Assert.IsNotNull(getResponse.Value.Data.TtlInSeconds);
            Console.WriteLine(getResponse.Value.Data.TtlInSeconds);

            // getall
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }

            // delete
            await recordSetAResource.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync("a");
            Assert.IsFalse(flag);
        }

        [Test]
        public async Task CaaRecordE2E()
        {
            var collection = _dnsZone.GetRecordSetCaas();
            string name = "caa";
            var recordSetCaaResource = await collection.CreateOrUpdateAsync(WaitUntil.Completed, name, new CaaRecordSetData() { });
            Assert.IsNotNull(recordSetCaaResource);
            Assert.IsNotNull(recordSetCaaResource.Value.Data.ETag);
            Assert.AreEqual(name, recordSetCaaResource.Value.Data.Name);
            Assert.AreEqual("Succeeded", recordSetCaaResource.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/CAA", recordSetCaaResource.Value.Data.ResourceType.Type);

            // exist
            bool flag = await collection.ExistsAsync("caa");
            Assert.IsTrue(flag);

            // get
            var getResponse = await collection.GetAsync(name);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(getResponse.Value.Data.TtlInSeconds);
            Console.WriteLine(getResponse.Value.Data.TtlInSeconds);

            // getall
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }

            // delete
            await recordSetCaaResource.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync("caa");
            Assert.IsFalse(flag);
        }

        [Test]
        public async Task CnameRecordE2E()
        {
            var collection = _dnsZone.GetRecordSetCnames();
            string name = "cname";
            var recordSetCnameResource = await collection.CreateOrUpdateAsync(WaitUntil.Completed, name, new CnameRecordSetData() { });
            Assert.IsNotNull(recordSetCnameResource);
            Assert.IsNotNull(recordSetCnameResource.Value.Data.ETag);
            Assert.AreEqual(name, recordSetCnameResource.Value.Data.Name);
            Assert.AreEqual("Succeeded", recordSetCnameResource.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/CNAME", recordSetCnameResource.Value.Data.ResourceType.Type);

            // exist
            bool flag = await collection.ExistsAsync("cname");
            Assert.IsTrue(flag);

            // get
            var getResponse = await collection.GetAsync(name);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(getResponse.Value.Data.TtlInSeconds);
            Console.WriteLine(getResponse.Value.Data.TtlInSeconds);

            // getall
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }

            // delete
            await recordSetCnameResource.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync("cname");
            Assert.IsFalse(flag);
        }

        [Test]
        public async Task MxRecordE2E()
        {
            var collection = _dnsZone.GetRecordSetMXes();
            string name = "mx";
            var recordSetMXResource = await collection.CreateOrUpdateAsync(WaitUntil.Completed, name, new MXRecordSetData() { });
            Assert.IsNotNull(recordSetMXResource);
            Assert.IsNotNull(recordSetMXResource.Value.Data.ETag);
            Assert.AreEqual(name, recordSetMXResource.Value.Data.Name);
            Assert.AreEqual("Succeeded", recordSetMXResource.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/MX", recordSetMXResource.Value.Data.ResourceType.Type);

            // exist
            bool flag = await collection.ExistsAsync("mx");
            Assert.IsTrue(flag);

            // get
            var getResponse = await collection.GetAsync(name);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(getResponse.Value.Data.TtlInSeconds);
            Console.WriteLine(getResponse.Value.Data.TtlInSeconds);

            // getall
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }

            // delete
            await recordSetMXResource.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync("mx");
            Assert.IsFalse(flag);
        }

        [Test]
        public async Task NsRecordE2E()
        {
            string _recordSetName = "ns";
            var collection = _dnsZone.GetRecordSetNS();
            var recordSetNSResource = await collection.CreateOrUpdateAsync(WaitUntil.Completed, _recordSetName, new NSRecordSetData() { });
            Assert.IsNotNull(recordSetNSResource);
            Assert.IsNotNull(recordSetNSResource.Value.Data.ETag);
            Assert.AreEqual(_recordSetName, recordSetNSResource.Value.Data.Name);
            Assert.AreEqual("Succeeded", recordSetNSResource.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/NS", recordSetNSResource.Value.Data.ResourceType.Type);

            // exist
            bool flag = await collection.ExistsAsync(_recordSetName);
            Assert.IsTrue(flag);

            // get
            var getResponse = await collection.GetAsync(_recordSetName);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(getResponse.Value.Data.TtlInSeconds);
            Console.WriteLine(getResponse.Value.Data.TtlInSeconds);

            // getall
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }

            // delete
            await recordSetNSResource.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync(_recordSetName);
            Assert.IsFalse(flag);
        }

        [Test]
        public async Task PtrRecordE2E()
        {
            var collection = _dnsZone.GetRecordSetPtrs();
            string name = "ptr";
            var recordSetPtrResource = await collection.CreateOrUpdateAsync(WaitUntil.Completed, name, new PtrRecordSetData() { });
            Assert.IsNotNull(recordSetPtrResource);
            Assert.IsNotNull(recordSetPtrResource.Value.Data.ETag);
            Assert.AreEqual(name, recordSetPtrResource.Value.Data.Name);
            Assert.AreEqual("Succeeded", recordSetPtrResource.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/PTR", recordSetPtrResource.Value.Data.ResourceType.Type);

            // exist
            bool flag = await collection.ExistsAsync("ptr");
            Assert.IsTrue(flag);

            // get
            var getResponse = await collection.GetAsync(name);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(getResponse.Value.Data.TtlInSeconds);
            Console.WriteLine(getResponse.Value.Data.TtlInSeconds);

            // getall
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }

            // delete
            await recordSetPtrResource.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync("ptr");
            Assert.IsFalse(flag);
        }

        [Test]
        public async Task SoaRecordE2E()
        {
            // exist
            bool result = await _dnsZone.GetRecordSetSoas().ExistsAsync("@");
            Assert.IsTrue(result);

            // get
            var getResponse = await _dnsZone.GetRecordSetSoas().GetAsync("@");
            Assert.IsNotNull(getResponse);
            Assert.AreEqual("@", getResponse.Value.Data.Name);
            Assert.AreEqual("Succeeded", getResponse.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/SOA", getResponse.Value.Data.ResourceType.Type);
            Assert.IsNotNull(getResponse.Value.Data.TtlInSeconds);
            Console.WriteLine(getResponse.Value.Data.TtlInSeconds);

            // getall
            await foreach (var item in _dnsZone.GetRecordSetSoas().GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }
        }

        [Test]
        public async Task SrvRecordE2E()
        {
            var collection = _dnsZone.GetRecordSetSrvs();
            string name = "srv";
            var recordSetSrvResource = await collection.CreateOrUpdateAsync(WaitUntil.Completed, name, new SrvRecordSetData() { });
            Assert.IsNotNull(recordSetSrvResource);
            Assert.IsNotNull(recordSetSrvResource.Value.Data.ETag);
            Assert.AreEqual(name, recordSetSrvResource.Value.Data.Name);
            Assert.AreEqual("Succeeded", recordSetSrvResource.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/SRV", recordSetSrvResource.Value.Data.ResourceType.Type);

            // exist
            bool flag = await collection.ExistsAsync("srv");
            Assert.IsTrue(flag);

            // get
            var getResponse = await collection.GetAsync(name);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(getResponse.Value.Data.TtlInSeconds);
            Console.WriteLine(getResponse.Value.Data.TtlInSeconds);

            // getall
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }

            // delete
            await recordSetSrvResource.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync("srv");
            Assert.IsFalse(flag);
        }

        [Test]
        public async Task TxtRecordE2E()
        {
            var collection = _dnsZone.GetRecordSetTxts();
            string name = "txt";
            var recordSetTxtResource = await collection.CreateOrUpdateAsync(WaitUntil.Completed, name, new TxtRecordSetData() { });
            Assert.IsNotNull(recordSetTxtResource);
            Assert.IsNotNull(recordSetTxtResource.Value.Data.ETag);
            Assert.AreEqual(name, recordSetTxtResource.Value.Data.Name);
            Assert.AreEqual("Succeeded", recordSetTxtResource.Value.Data.ProvisioningState);
            Assert.AreEqual("dnszones/TXT", recordSetTxtResource.Value.Data.ResourceType.Type);

            // exist
            bool flag = await collection.ExistsAsync("txt");
            Assert.IsTrue(flag);

            // get
            var getResponse = await collection.GetAsync(name);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(getResponse.Value.Data.TtlInSeconds);
            Console.WriteLine(getResponse.Value.Data.TtlInSeconds);

            // getall
            await foreach (var item in collection.GetAllAsync())
            {
                Console.WriteLine(item.Data.Name);
            }

            // delete
            await recordSetTxtResource.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync("txt");
            Assert.IsFalse(flag);
        }
    }
}
