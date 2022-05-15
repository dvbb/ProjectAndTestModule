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
                mc.SayStartHandler.Invoke();
                mc.SayStartHandler += new Audience().Listening;
                mc.SayStartHandler += new Cat().Miao;
                mc.SayStartHandler += new Cat().Miao;
                mc.SayStartHandler += new Cat().Miao;
                mc.SayStartHandler += new Cat().Miao;
                mc.SayStartHandler += new Dog().Wang;
                mc.SayStartVer2();

                // 由于event的限制，下面两行会报编译器异常
                //mc.SayStartHandlerEvnet = new Cat().Miao;
                //mc.SayStartHandlerEvnet.Invoke();
                mc.SayStartHandlerEvnet += new Cat().Miao;
                mc.SayStartHandlerEvnet -= new Cat().Miao;
            }
            #endregion
        }
    }
}
