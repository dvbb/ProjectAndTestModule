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
            students.Add(new Student() { Number = 20201113,Name = "naoki", Sex = Sex.Female, Age = 47});
            students.Add(new Student() { Number = 20201114,Name = "Yukiri", Sex = Sex.Female, Age = 16});
            students.Add(new Student() { Number = 20201115,Name = "Fuyumi", Sex = Sex.Female, Age = 16});
            students.Add(new Student() { Number = 20221114,Name = "Haruka", Sex = Sex.Female, Age = 14});
            students.Add(new Student() { Number = 20201116,Name = "Kitana", Sex = Sex.Male, Age = 16});
            students.Add(new Student() { Number = 20201117,Name = "elen", Sex = Sex.Male, Age = 43});
            students.Add(new Student() { Number = 20201118,Name = "wake", Sex = Sex.Male, Age = 15});
            students.Add(new Student() { Number = 20201119, Name = "wore", Sex = Sex.Male, Age = 27});
            students.Add(new Student() { Number = 20201120, Name = "kie", Sex = Sex.Male, Age = 34});
            students.Add(new Student() { Number = 20201121, Name = "green", Sex = Sex.Male, Age = 65});
            students.Add(new Student() { Number = 20201122, Name = "mike", Sex = Sex.Male, Age = 32});
            return students;
        }
    }
    internal enum Sex
    {
        Male,Female 
    }
}
