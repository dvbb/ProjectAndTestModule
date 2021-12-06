using ConsoleApp.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests.DataStructure
{
    public class LinkedListTest
    {
        [Test]
        public void LinkedListCommonTest()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(10);
            list.AddLast(11);
            list.AddLast(12);
            list.AddLast(14);
            list.AddLast(100);
            Console.WriteLine("direct output LinkedList: " + list);
            Console.WriteLine("current LinkedList: "+Formatting.LinkedListToString(list)); 
            Console.WriteLine("LinkedList first node value: " + list.First.Value.ToString());
            Console.WriteLine("LinkedList last node value: " + list.Last.Value.ToString());

            Console.WriteLine("\nfind [Node-12], and add a new [Node-90] after it");
            list.AddAfter(list.Find(12), 90);
            Console.WriteLine("current LinkedList: "+Formatting.LinkedListToString(list));

            Console.WriteLine("\nfind [Node-12], and add a new [Node-80] after it  ");
            list.AddBefore(list.Find(12), 80);
            Console.WriteLine("current LinkedList: " + Formatting.LinkedListToString(list));

            Console.WriteLine("\nfind [Node-12] remove it while remove first node and last node");
            list.Remove(list.Find(12));
            list.RemoveFirst();
            list.RemoveLast();
            Console.WriteLine("current LinkedList: " + Formatting.LinkedListToString(list));

            Console.WriteLine("\nlist contain 10: " + list.Contains(10));
        }
    }
}
