using ConsoleApp.FakesSample;
using ConsoleApp.FakesSample.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests
{
    public class FakesTests
    {
        [Test]
        [Ignore("Error sample")]
        public void WithoutWtubTest()
        {
            Stock stock = new Stock();
            int price = stock.GetCompany("cisco");
            Assert.AreEqual(458, price);
        }

        [Test]
        public void StubTest1()
        {
            IStock stockfeed =
               new  ConsoleApp.FakesSample.Fakes.StubIStock()
                {
                    GetCompanyString = (company) => { return 458; }
                };
            int price = stockfeed.GetCompany("cisco");
            Assert.AreEqual(458, price);
        }

        [Test]
        public void StubTest2()
        {
            IStock stock =
                new ConsoleApp.FakesSample.Fakes.StubIStock()
                {
                    GetHighest = () => { return 866; }
                };
            int highestPrice = stock.GetHighest();
            Assert.AreEqual(866, highestPrice);
        }

        [Test]
        public void StubTest3()
        {
            IStock stockfeed =
                 new ConsoleApp.FakesSample.Fakes.StubIStock()
                 {
                    GetDetailString = (name) =>
                    {
                        Detail detail = new Detail();
                        detail.Name = "hi";
                        return detail;
                    }
                };
            Detail detail = stockfeed.GetDetail("cisco");
            Detail expected = new Detail();
            expected.Name = "hi";
            Assert.AreEqual(expected.Name, detail.Name);
        }

        [Test]
        [Ignore("Error sample")]
        public void WithoutShims()
        {
            int number = TestForFakesUtilities.GetRandomNumber();
            Assert.AreEqual(50, number);
        }

        [Test]
        public void ShimsTest1()
        {
            using (ShimsContext.Create())
            {
                ShimTestForFakesUtilities.GetRandomNumber = () => { return 50; };
                int number = TestForFakesUtilities.GetRandomNumber();
                Assert.AreEqual(50, number);
            }
        }
    }
}
