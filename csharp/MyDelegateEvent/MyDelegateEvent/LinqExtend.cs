using System;
using System.Collections.Generic;
using System.Text;

namespace MyDelegateEvent
{
    internal static class LinqExtend
    {
        public static List<Student> EventWhere(List<Student> studentList, Func<Student, bool> func)
        {
            List<Student> result = new List<Student>();
            foreach (Student student in studentList)
            {
                if (func.Invoke(student))
                {
                    result.Add(student);
                }
            }
            return result;
        }
    }
}
