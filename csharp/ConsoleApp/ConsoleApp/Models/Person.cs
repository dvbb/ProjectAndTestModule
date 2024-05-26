using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    internal class Person
    {
        public static string _StaticField  = "value1";
        public static string StaticField1 { get; set; } = "value1";

        public Person()
        {
            Console.WriteLine("Person construction method");
        }
        public static string StaticField2 { get; set; } = "value2";

    }
}
