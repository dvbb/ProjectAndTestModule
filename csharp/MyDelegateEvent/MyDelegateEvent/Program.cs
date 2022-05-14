using MyDelegateEvent.Character;
using System;
using static MyDelegateEvent.DelegateTest;

namespace MyDelegateEvent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //LambdaTest lambdaTest = new LambdaTest();
            //lambdaTest.Start();

            //LinqTest linqTest = new LinqTest();
            //linqTest.CustomWhereTest();

            //MulticastDelegate eventTest = new MulticastDelegate();
            //eventTest.Start();

            MC mc = new MC();
            mc.SayStartVer1();
            Console.WriteLine("\n\nuse muliticast delegate ");
            //使用多播委托传递参数，无需修改方法体源码，使用更加灵活
            mc.SayStartHandler += new Singer().Singing;
            mc.SayStartHandler += new Audience().Listening;
            mc.SayStartHandler += new Cat().Miao;
            mc.SayStartHandler += new Cat().Miao;
            mc.SayStartHandler += new Cat().Miao;
            mc.SayStartHandler += new Cat().Miao;
            mc.SayStartHandler += new Dog().Wang;
            mc.SayStartVer2();
        }
    }
}
