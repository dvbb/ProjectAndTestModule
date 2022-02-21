using AzureSample;
using System;
using NUnit.Framework;

namespace Track1
{
    public class Tests : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine(subscription);
            Assert.Pass();
        }
    }
}