using System;
using System.Collections.Generic;
using System.Text;

namespace MyAttribute.Models
{
    internal class Teacher : Person
    {
        public Teacher() { Title = "primary teachers"; }

        public string Title { get; set; }

        public string SelfIntroduction(string value)
        {
            Console.WriteLine($"Hi, i am {this.Name}, {this.Title}");
            return value;
        }
    }
}
