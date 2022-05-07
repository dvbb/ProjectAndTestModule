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

            LinqTest linqTest = new LinqTest();
            linqTest.CustomWhereTest();
        }
    }
}
