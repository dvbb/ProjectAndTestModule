using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace ConsoleAppTests.Polymorphism
{
    internal class VirtualMethodTests
    {
        [Test]
        public void VirtualMethodTest()
        {
            Person chinese = new Chinese();
            chinese.Name = "wang";
            Console.WriteLine("当Person chinese = new Chinese()时。所执行的还是父类的show方法。");
            Console.WriteLine("执行chinese的show方法：");
            chinese.Show();
            Console.WriteLine();

            Console.WriteLine("执行chinese的VirtualShow方法：");
            chinese.VirtualShow();
        }


        private class Person
        {
            public Person() { }

            public string Name { get; set; }
            public void Show()
            {
                Console.WriteLine($"Person {Name}");
            }

            public virtual void VirtualShow()
            {
                Console.WriteLine($"Person {Name}");
            }
        }

        private class Chinese : Person
        {
            public void Show()
            {
                Console.WriteLine($"Chinese {Name}");
            }

            public override void VirtualShow()
            {
                Console.WriteLine($"Chinese {Name}");
            }
        }
    }
}
