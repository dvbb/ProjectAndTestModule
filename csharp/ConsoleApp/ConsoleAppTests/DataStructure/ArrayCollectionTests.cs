using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleAppTests.DataStructure
{
    public class ArrayCollectionTests
    {
        [Test]
        public void ArrayCommonTest()
        {
            int[] nums = new int[5];
        }

        [Test]
        public void ArrayListCommonTest()
        {
            ArrayList list = new ArrayList();
            list.Add(1);
            list.Add(true);
            list.Add(new ArrayList() { });
            list.Add("hi");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            ArrayList list2 = new ArrayList();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                list2.Add(0);
            }
            sw.Stop();
            Console.WriteLine($"ArrayList add 10000 number. time cost: {sw.Elapsed}");
        }

        [Test]
        public void ListCommonTest()
        {
            List<int> list = new List<int>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(0);
            }
            sw.Stop();
            Console.WriteLine($"ArrayList add 10000 number. time cost: {sw.Elapsed}");
        }

        [Test]
        public void QueueCommonTest()
        {
            Queue queue = new Queue();
            Queue<string> queue2 = new Queue<string>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue("third");
            object[] objects = queue.ToArray();
            Console.WriteLine($"queue's head: {queue.Peek()}");
            Console.WriteLine();
            while (queue.Count != 0)
            {
                Console.WriteLine(queue.Dequeue());
            }
        }

        [Test]
        public void StackCommonTest()
        {
            Stack stack = new Stack();
            Stack<string> stack2 = new Stack<string>();
            stack.Push(1);
            stack.Push(2);
            stack.Push("third");
            object[] objects = stack.ToArray();
            Console.WriteLine($"stack's top: {stack.Peek()}");
            Console.WriteLine();
            while (stack.Count != 0)
            {
                Console.WriteLine(stack.Pop());
            }
        }
    }
}
