using System;
using System.Collections.Generic;
using System.Text;

namespace MyAttribute.Models
{
    [Foo]
    [Foo("str")]
    [Foo(123)]
    [Foo(Remark = "1")]
    [Foo(Description = "some description")]
    [Foo(Remark = "1", Description = "some description 2")]
    internal class Student : Person
    {
        public Student() { Grade = 1; }

        [Foo("str")]
        public int Grade { get; set; }

        [Foo("str")]
        [return: Foo("str")]
        public string SelfIntroduction([Foo("str")] string value)
        {
            Console.WriteLine($"Hi, i am {this.Name}, {this.Grade} Grade.");
            return value;
        }
    }
}
