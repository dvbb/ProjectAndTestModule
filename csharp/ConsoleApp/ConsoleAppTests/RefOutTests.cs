using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleAppTests
{
    internal class RefOutTests
    {

        [Test]
        public void RefTest()
        {
            int num1 = 1;
            int num2 = 99;
            Console.WriteLine("num1:" + num1);
            Console.WriteLine("num2:" + num2);
            Console.WriteLine("swap");
            Swap(ref num1, ref num2);
            Console.WriteLine("num1:" + num1);
            Console.WriteLine("num2:" + num2);
            Console.WriteLine();
        }

        [Test]
        public void OutTest()
        {
            int num1 = 1;
            int num2 = 99;
            int num3 = 150;
            Console.WriteLine("num1:" + num1);
            Console.WriteLine("num2:" + num2);
            Console.WriteLine("num3:" + num3);
            Console.WriteLine("swap");
            Swap(ref num1, ref num2, out num3);
            Console.WriteLine("num1:" + num1);
            Console.WriteLine("num2:" + num2);
            Console.WriteLine("num3:" + num3);
            Console.WriteLine();
        }

        private void Swap(ref int num1, ref int num2)
        {
            int temp = num1;
            num1 = num2;
            num2 = temp;
        }

        private void Swap(ref int num1, ref int num2, out int num3)
        {
            //Console.WriteLine(num3); // 这一行会报错: Use of unassigned out parameter 'num3'
            num3 = num1;
        }
    }
}
