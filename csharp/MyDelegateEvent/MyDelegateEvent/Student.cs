using System;
using System.Collections.Generic;
using System.Text;

namespace MyDelegateEvent
{
    internal class Student
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public  Sex Sex { get; set; }
        public int Age { get; set; }

        public static List<Student> GetDefaultStudents()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student() { Number = 20201114,Name = "Yukiri", Sex = Sex.Female, Age = 16});
            students.Add(new Student() { Number = 20201114,Name = "Fuyumi", Sex = Sex.Female, Age = 16});
            students.Add(new Student() { Number = 20201114,Name = "Haruka", Sex = Sex.Female, Age = 14});
            students.Add(new Student() { Number = 20201114,Name = "Kitana", Sex = Sex.Male, Age = 16});
            return students;
        } 
    }
    internal enum Sex
    {
        Male,Female 
    }
}
