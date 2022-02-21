using AzureSample;
using System;
using NUnit.Framework;

namespace Fluent
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