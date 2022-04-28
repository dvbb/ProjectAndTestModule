using System;
using static MyDelegateEvent.DelegateTest;

namespace MyDelegateEvent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DelegateTest delegateTest = new DelegateTest();
            NoReturnNoPara noReturnNoPara = new NoReturnNoPara(delegateTest.DoNothing);
            noReturnNoPara.Invoke();
        }
    }
}
