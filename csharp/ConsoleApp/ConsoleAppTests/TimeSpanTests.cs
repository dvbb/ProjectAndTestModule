using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests
{
    internal class TimespanTests
    {
        [Test]
        public void TimespanTest()
        {
            TimeSpan time = new TimeSpan();
            Console.WriteLine(time);

            // ctor for one parameter is ticks
            time = new TimeSpan(12);
            Console.WriteLine(time);

            time = new TimeSpan(315,25,28);
            Console.WriteLine(time);
            Console.WriteLine(time.Days);
            Console.WriteLine(time.Hours);
            Console.WriteLine(time.Seconds);
            Console.WriteLine(time.Ticks);
        }
    }
}
