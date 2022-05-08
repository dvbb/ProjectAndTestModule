using System;
using System.Collections.Generic;
using System.Text;

namespace MyDelegateEvent
{
    internal class MulticastDelegate
    {
        public delegate void NoReturnNoPara();
        public void SayHello() { Console.WriteLine("Hello"); }
        public static void SayHelloStatic() { Console.WriteLine("Hello static."); }
        public void Start()
        {
            //委托都是多播委托，继承自MulticastDelegate
            //一个委托实例包含多个方法，称之为多播委托实例

            // += 给委托实例增加方法，形成方法链 Invoke时会按照顺序执行
            NoReturnNoPara method = new NoReturnNoPara(this.SayHello);
            method += new NoReturnNoPara(this.SayHello);
            method += new NoReturnNoPara(MulticastDelegate.SayHelloStatic);
            method += new NoReturnNoPara(new Student().SayHello);
            method += new NoReturnNoPara(Student.SayHelloStatic);
            method += new NoReturnNoPara(() => Console.WriteLine("Hello lambda."));

            method.Invoke();
            Console.WriteLine("********** -= *************");

            // -= 可以移除委托实例的方法
            // 从方法链尾部开始匹配，遇到第一个完全匹配的，则移除
            //  完全匹配：method 和 target 完全相同。
            //  例如：lambda表达式是完全不同的方法，则无法移除。  new Student().SayHello是不同的两个实例，target不同，也无法移除。
            method -= new NoReturnNoPara(this.SayHello);
            method -= new NoReturnNoPara(MulticastDelegate.SayHelloStatic);
            method -= new NoReturnNoPara(new Student().SayHello);
            method -= new NoReturnNoPara(Student.SayHelloStatic);
            method -= new NoReturnNoPara(() => Console.WriteLine("Hello lambda."));
            method.Invoke();

            //Tag：多播委托无法用异步多线程调用
            // 但可以使用GetInvocationList()获取多播委托里面的单个方法，再去调用
            Console.WriteLine("********** GetInvocationList *************");
            foreach (NoReturnNoPara item in method.GetInvocationList())
            {
                item.Invoke();
            }
        }
    }
}
