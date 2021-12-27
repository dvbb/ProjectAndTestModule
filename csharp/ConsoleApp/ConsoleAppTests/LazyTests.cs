using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests
{
    public class LazyTests
    {
        [Test]
        public void LazyBasicTest()
        {
            Lazy<Student> student = new Lazy<Student>();
            if (!student.IsValueCreated)
            {
                Console.WriteLine("student is not Init");
            }
            string temp = string.IsNullOrEmpty(student.Value.Name) ? "null" : student.Value.Name;
            Console.WriteLine($"Name: {temp}");
            student.Value.Name = "dvbb";
            student.Value.Id = 12033514;
            Console.WriteLine(student.Value.Name);
            Console.WriteLine(student.Value.Id);
        }

        private class Student
        {
            public string Name { get; set; }

            public int Id { get; set; }

            public Student() { }

            public Student(string name, int id)
            {
                Name = name;
                Id = id;
            }
        }
    }
}
