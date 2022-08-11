using MyAttribute.Models;
using System;

namespace MyAttribute
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            /// 如果只是用特性标记，无论标记的是类、属性、字段、方法、返回值
            /// 正常运行代码时都与没有定义特性时没有任何区别
            /// 但IL中会对应的类中会出现.custom元素，metadata清单会有对特性的相关描述
            /// 然后可以通过 反射reflection读取使用
            /// 特性本质效果是给某个方法属性等加上一个额外的值，不会破坏封装，是AOP的一种实现方法
            #region Basic

            Student stu = new Student();
            stu.SelfIntroduction("something");

            Teacher teacher = new Teacher();
            teacher.SelfIntroduction("something");

            #endregion
        }
    }
}
