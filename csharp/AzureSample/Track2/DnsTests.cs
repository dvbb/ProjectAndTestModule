using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure;
using Azure.ResourceManager;
using Azure.ResourceManager.Dns;
using Azure.ResourceManager.Dns.Models;
using Azure.ResourceManager.Resources;
using NUnit.Framework;
using Track2.Helper;

namespace Track2
{
    internal class DnsTests : Track2TestBase
    {
        private ResourceGroupResource _resourceGroup;
        private DnsZoneResource _dnsZone;

        [OneTimeSetUp]
        public async Task GlobalSetUp()
        {
            // Create a resource group
            string rgName = "Dns-Custom-RG-0000";
            _resourceGroup = await CreateResourceGroup(rgName, DefaultLocation);

            //Create DnsZone
            string dnsZoneName = "220901sample0000.com";
            DnsZoneData data = new DnsZoneData("Global") { };
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
        public async Task AaaaRecordOperationTest()
        {
            var collection = _dnsZone.GetDnsAaaaRecords();
            string aaaaRecordName = Recording.GenerateAssetName("aaaa");
            string ipv6AddressValue1 = "3f0d:8079:32a1:9c1d:dd7c:afc6:fc15:d55";
            string ipv6AddressValue2 = "3f0d:8079:32a1:9c1d:dd7c:afc6:fc15:d66";

            // CreateOrUpdate
            var data = new DnsAaaaRecordData()
            {
                TtlInSeconds = 3600,
                DnsAaaaRecords =
                {
                    new DnsAaaaRecordInfo()
                    {
                        IPv6Address =  IPAddress.Parse(ipv6AddressValue1)
                    },
                    new DnsAaaaRecordInfo()
                    {
                        IPv6Address = IPAddress.Parse(ipv6AddressValue2)
                    },
                }
            };
            var aaaaRecord = await collection.CreateOrUpdateAsync(WaitUntil.Completed, aaaaRecordName, data);
            ValidateRecordBaseInfo(aaaaRecord.Value.Data, aaaaRecordName);
            Assert.AreEqual("dnszones/AAAA", aaaaRecord.Value.Data.ResourceType.Type.ToString());
            Assert.AreEqual(3600, aaaaRecord.Value.Data.TtlInSeconds);
            Assert.AreEqual(ipv6AddressValue1, aaaaRecord.Value.Data.DnsAaaaRecords[0].IPv6Address.ToString());
            Assert.AreEqual(ipv6AddressValue2, aaaaRecord.Value.Data.DnsAaaaRecords[1].IPv6Address.ToString());

            // Exist
            bool flag = await collection.ExistsAsync(aaaaRecordName);
            Assert.IsTrue(flag);

            // Update
            var updateResponse = await aaaaRecord.Value.UpdateAsync(new DnsAaaaRecordData() { TtlInSeconds = 7200 });
            Assert.AreEqual(7200, updateResponse.Value.Data.TtlInSeconds);

            // Get
            var getResponse = await collection.GetAsync(aaaaRecordName);
            ValidateRecordBaseInfo(getResponse.Value.Data, aaaaRecordName);
            Assert.AreEqual(7200, getResponse.Value.Data.TtlInSeconds);
            Assert.AreEqual(ipv6AddressValue1, getResponse.Value.Data.DnsAaaaRecords[0].IPv6Address.ToString());
            Assert.AreEqual(ipv6AddressValue2, getResponse.Value.Data.DnsAaaaRecords[1].IPv6Address.ToString());

            // GetAll
            var list = await collection.GetAllAsync().ToEnumerableAsync();
            Assert.IsNotEmpty(list);
            ValidateRecordBaseInfo(list.First(item => item.Data.Name == aaaaRecordName).Data, aaaaRecordName);

            // Delete
            await aaaaRecord.Value.DeleteAsync(WaitUntil.Completed);
            flag = await collection.ExistsAsync(aaaaRecordName);
            Assert.IsFalse(flag);
        }

        [Test]
        public async Task GetAllRecords()
        {
            string dnsZoneName = $"{Recording.GenerateAssetName("sample")}.com";
            var dnszone = _dnsZone;

            // Add some aaaaRecord
            var aaaaRecord1 = dnszone.GetDnsAaaaRecords().CreateOrUpdateAsync(WaitUntil.Completed, Recording.GenerateAssetName("aaaa"), new DnsAaaaRecordData()
            {
                TtlInSeconds = 3600,
                DnsAaaaRecords =
                {
                    new DnsAaaaRecordInfo()
                    {
                        IPv6Address = IPAddress.Parse("3f0d:8079:32a1:9c1d:dd7c:afc6:fc15:d55")
                    },
                    new DnsAaaaRecordInfo()
                    {
                        IPv6Address = IPAddress.Parse("3f0d:8079:32a1:9c1d:dd7c:afc6:fc15:d56")
                    },
                }
            });
            var aaaaRecord2 = dnszone.GetDnsAaaaRecords().CreateOrUpdateAsync(WaitUntil.Completed, Recording.GenerateAssetName("aaaa"), new DnsAaaaRecordData()
            {
                TtlInSeconds = 3600,
                DnsAaaaRecords =
                {
                    new DnsAaaaRecordInfo()
                    {
                        IPv6Address = IPAddress.Parse("3f0d:8079:32a1:9c1d:dd7c:afc6:fc15:d57")
                    },
                    new DnsAaaaRecordInfo()
                    {
                        IPv6Address = IPAddress.Parse("3f0d:8079:32a1:9c1d:dd7c:afc6:fc15:d58")
                    },
                }
            });

            // Add some caaRecord
            var caaRecord1 = dnszone.GetDnsCaaRecords().CreateOrUpdateAsync(WaitUntil.Completed, Recording.GenerateAssetName("caa"), new DnsCaaRecordData()
            {
                TtlInSeconds = 3600,
                DnsCaaRecords =
                {
                    new DnsCaaRecordInfo()
                    {
                        Flags = 1,
                        Tag = "test1",
                        Value = "caa1.contoso.com"
                    },
                    new DnsCaaRecordInfo()
                    {
                        Flags = 2,
                        Tag = "test2",
                        Value = "caa2.contoso.com"
                    }
                }
            });
            var caaRecord2 = dnszone.GetDnsCaaRecords().CreateOrUpdateAsync(WaitUntil.Completed, Recording.GenerateAssetName("caa"), new DnsCaaRecordData()
            {
                TtlInSeconds = 3600,
                DnsCaaRecords =
                {
                    new DnsCaaRecordInfo()
                    {
                        Flags = 3,
                        Tag = "test3",
                        Value = "caa3.contoso.com"
                    },
                    new DnsCaaRecordInfo()
                    {
                        Flags = 4,
                        Tag = "test4",
                        Value = "caa4.contoso.com"
                    }
                }
            });

            var recordSets = await dnszone.GetAllRecordDataAsync().ToEnumerableAsync();
            Assert.IsNotEmpty(recordSets);
            Console.WriteLine(recordSets[0].DnsNSRecords);
            Console.WriteLine(recordSets[1].DnsSoaRecordInfo);
            Console.WriteLine(recordSets[2].DnsAaaaRecords);
            Console.WriteLine(recordSets[3].DnsCaaRecords);
        }

        private void ValidateRecordBaseInfo(DnsBaseRecordData recordData, string recordName)
        {
            Assert.IsNotNull(recordData);
            Assert.IsNotNull(recordData.ETag);
            Assert.AreEqual(recordName, recordData.Name);
        }
    }
}
