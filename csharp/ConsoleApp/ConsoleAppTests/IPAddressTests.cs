using ConsoleAppTests.Helper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConsoleAppTests
{
    internal class IPAddressTests : TestBase
    {
        [Test]
        public void IPAddressTest()
        {
            string value1 = "192.168.0.1";
            string value2 = "20.98.144.224/27";

            IPAddress address1 = IPAddress.Parse(value1);
            string log = RecordErrorMessage(() => IPAddress.Parse(value2));
            Console.WriteLine(log);
        }
    }
}
