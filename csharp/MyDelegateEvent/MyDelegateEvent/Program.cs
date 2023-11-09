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

            #region Multicast usage
            {
                MC mc = new MC();
                mc.SayStartVer1();

                //使用多播委托传递参数，无需修改方法体源码，使用更加灵活
                Console.WriteLine("\n\nuse muliticast delegate ");
                mc.SayStartHandler = null;
                mc.SayStartHandler += new Singer().Singing;
                Console.WriteLine("\nInvoke [mc.SayStartHandler]");
                mc.SayStartHandler.Invoke();
                mc.SayStartHandler += new Audience().Listening;
                mc.SayStartHandler += new Cat().Miao;
                mc.SayStartHandler += new Cat().Miao;
                mc.SayStartHandler += new Cat().Miao;
                mc.SayStartHandler += new Cat().Miao;
                mc.SayStartHandler += new Dog().Wang;
                Console.WriteLine("\nInvoke [mc.SayStartVer2()]");
                mc.SayStartVer2();

                // 委托(Func & Action)可以直接执行Invoke(). 事件(Event)不可以直接执行Invoke().
                // 由于event的限制，下面两行会报编译器异常
                //mc.SayStartHandlerEvnet = new Cat().Miao;
                //mc.SayStartHandlerEvnet.Invoke();
                Console.WriteLine("\n\n event:");
                mc.SayStartHandlerEvnet += new Cat().Miao;
                mc.SayStartHandlerEvnet -= new Cat().Miao;
                mc.SayStartHandlerEvnet += new Cat().Miao;
                mc.SayStartHandlerEvnet += new Cat().Miao;
                mc.SayStartVerEvent();
            }
            #endregion
        }
    }
}
