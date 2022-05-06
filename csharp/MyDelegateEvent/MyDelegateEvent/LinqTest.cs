using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDelegateEvent
{
    public class LinqTest
    {
        public void CustomWhereTest()
        {
            List<Student> studentList = Student.GetDefaultStudents();
            //获取年龄为16的学生列表
            var list1 = studentList.Where(stu => stu.Age == 16);
            var list2 = LinqExtend.EventWhere(studentList, stu => stu.Age == 16);
        }
    }
}
