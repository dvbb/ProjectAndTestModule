using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    internal class Student : Person
    {
        public static string StaticField3 { get; set; } = "value3";
        public Student()
        {
            Console.WriteLine("Student construction method");
        }
        public static string StaticField4 { get; set; } = "value4";

    }
}
