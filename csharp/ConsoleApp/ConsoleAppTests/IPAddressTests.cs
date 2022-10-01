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
            string value3 = "";

            IPAddress address1 = IPAddress.Parse(value1);
            string record1 = RecordErrorMessage(() => IPAddress.Parse(value2));
            string record2 = RecordErrorMessage(() => IPAddress.Parse(value3));
            Console.WriteLine($"{record1}\n\n");
            Console.WriteLine($"{record2}\n\n");
        }
    }
}
