using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests
{
    public class DateTimeTests
    {
        [Test]
        public void DateTimeTest()
        {
            Console.WriteLine($"Max: {DateTime.MaxValue}");
            Console.WriteLine($"Min: {DateTime.MinValue}");
            Console.WriteLine($"Now: {DateTime.Now}");
            Console.WriteLine();

            DateTime dateTime = new DateTime(2020, 12, 14, 08, 45, 25);
            Console.WriteLine(dateTime);
            Console.WriteLine($"dateTime > Now: {dateTime > DateTime.Now}");
            TimeSpan time = DateTime.Now - dateTime;
            Console.WriteLine($"Now - dateTime: {time} || {time.Days}Days {time.Hours}h-{time.Minutes}m-{time.Seconds}s-{time.Milliseconds}ms");
            Console.WriteLine();

            Console.WriteLine("Parse():");
            string str1 = "08.03.2017";
            string str2 = "65184751656";

            DateTime.TryParse(str2,out dateTime);
            Console.WriteLine($"Parse[{str1}]: {DateTime.Parse(str1)}");
            Console.WriteLine($"TryParse() error return: {dateTime}");
            Console.WriteLine();

            Console.WriteLine("Tostring():");
            string format = "yyyy-MM-dd hh:mm:ss";
            Console.WriteLine($"Now format with [{format}]: {DateTime.Now.ToString(format)}");
            Console.WriteLine($"Now format with [D]: {DateTime.Now.ToString("D")}");
            Console.WriteLine($"Now format with [d]: {DateTime.Now.ToString("d")}");
            Console.WriteLine($"Now format with [dddd]: {DateTime.Now.ToString("dddd")}");
        }
    }
}
