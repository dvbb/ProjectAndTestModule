using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyDelegateEvent
{
    public class LinqTest
    {
        public void CustomWhereTest()
        {
            List<Student> studentList = Student.GetDefaultStudents();
            //获取年龄为16的学生列表
            var list1 = studentList.Where(stu => stu.Age == 16);
            var list2 = LinqExtend.EventWhere(studentList, stu => stu.Age == 16);   //直接调用
            var list3 = studentList.EventWhere(stu => stu.Age == 16);   //扩展方法调用

            //获取年龄大于30的并展示
            {
                //List：一次性获取所有的结果，然后再console展示
                Console.WriteLine("************************ Start ****************************");
                var list4 = studentList.EventWhere(stu => { Thread.Sleep(300); return stu.Age > 30; });
                foreach (var item in list4)
                {
                    Console.WriteLine($"{item.Name}-{item.Age}");
                }
                Console.WriteLine("************************ End ****************************");
            }
            {
                //IEnumerable：需要展示时，才进入方法体获取一个结果
                Console.WriteLine("\n************************ Start ****************************");
                var list5 = studentList.EventWhereIEnumerable(stu => { Thread.Sleep(300); return stu.Age > 30; });
                foreach (var item in list5)
                {
                    Console.WriteLine($"{item.Name}-{item.Age}");
                }
                Console.WriteLine("************************ End ****************************");
            }
        }
    }
}
